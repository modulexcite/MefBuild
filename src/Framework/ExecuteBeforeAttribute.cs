﻿using System;
using System.Composition;
using System.Globalization;
using System.Reflection;

namespace MefBuild
{
    /// <summary>
    /// Specifies that a command will be automatically executed before a command with the given type.
    /// </summary>
    [MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ExecuteBeforeAttribute : ExportAttribute
    {
        internal const string PredefinedContractName = "ExecuteBefore";
        private readonly Type targetCommandType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteBeforeAttribute"/> class with the given <see cref="Type"/> of target command.
        /// </summary>
        public ExecuteBeforeAttribute(Type targetCommandType) : base(PredefinedContractName, typeof(Command))
        {
            this.targetCommandType = targetCommandType;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of target <see cref="Command"/>.
        /// </summary>
        public Type TargetCommandType
        {
            get { return this.targetCommandType; }
        }
    }
}
