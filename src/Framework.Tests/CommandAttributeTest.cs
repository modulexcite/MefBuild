﻿using System;
using System.Composition;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using Xunit;

namespace MefBuild
{
    public class CommandAttributeTest
    {
        [Fact]
        public void ClassIsMetadataAttributeDescribingConcreteCommandClass()
        {
            Assert.True(typeof(CommandAttribute).IsPublic);
            Assert.True(typeof(CommandAttribute).IsSealed);
            Assert.True(typeof(Attribute).IsAssignableFrom(typeof(CommandAttribute)));
            Assert.NotNull(typeof(CommandAttribute).GetCustomAttribute<MetadataAttributeAttribute>());
            Assert.Equal(AttributeTargets.Class, typeof(CommandAttribute).GetCustomAttribute<AttributeUsageAttribute>().ValidOn);
        }

        [Fact]
        public void ClassIsNotExportAttributeBecauseMefDoesNotCombineMetadataFromMultipleAttributes()
        {
            Assert.False(typeof(ExportAttribute).IsAssignableFrom(typeof(CommandAttribute)));
        }

        [Fact]
        public void DependsOnProvidesDependsOnCommandMetadata()
        {
            var context = new ContainerConfiguration().WithPart<TestCommand>().CreateContainer();
            var export = context.GetExport<ExportFactory<Command, CommandMetadata>>();
            Assert.Equal(typeof(TestCommand).GetCustomAttribute<CommandAttribute>().DependsOn, export.Metadata.DependsOn);
        }

        [Fact]
        public void ExecuteBeforeProvidesExecuteBeforeCommandMetadata()
        {
            var context = new ContainerConfiguration().WithPart<TestCommand>().CreateContainer();
            var export = context.GetExport<ExportFactory<Command, CommandMetadata>>();
            Assert.Equal(typeof(TestCommand).GetCustomAttribute<CommandAttribute>().ExecuteBefore, export.Metadata.ExecuteBefore);
        }

        [Fact]
        public void ExecuteAfterProvidesExecuteAfterCommandMetadata()
        {
            var context = new ContainerConfiguration().WithPart<TestCommand>().CreateContainer();
            var export = context.GetExport<ExportFactory<Command, CommandMetadata>>();
            Assert.Equal(typeof(TestCommand).GetCustomAttribute<CommandAttribute>().ExecuteAfter, export.Metadata.ExecuteAfter);
        }

        [Fact]
        public void SummaryProvidesSummaryCommandMetadata()
        {
            var context = new ContainerConfiguration().WithPart<TestCommand>().CreateContainer();
            var export = context.GetExport<ExportFactory<Command, CommandMetadata>>();
            Assert.Equal(typeof(TestCommand).GetCustomAttribute<CommandAttribute>().Summary, export.Metadata.Summary);
        }

        [Export(typeof(Command)), Command(
            DependsOn = new[] { typeof(StubCommand) },
            ExecuteBefore = new[] { typeof(StubCommand) },
            ExecuteAfter = new[] { typeof(StubCommand) },
            Summary = "Test Summary")]
        public class TestCommand : Command
        {
        }
    }
}
