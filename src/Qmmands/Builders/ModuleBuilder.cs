﻿using System;
using System.Collections.Generic;

namespace Qmmands
{
    /// <summary>
    ///     Allows for building <see cref="Module"/> objects using the <see cref="CommandService"/>.
    /// </summary>
    public sealed class ModuleBuilder
    {
        /// <summary>
        ///     Gets or sets the name of the <see cref="Module"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description of the <see cref="Module"/>.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Qmmands.RunMode"/> of the <see cref="Module"/>.
        /// </summary>
        public RunMode? RunMode { get; set; }

        /// <summary>
        ///     Gets or sets whether the <see cref="Command"/>s in the <see cref="Module"/> should ignore extra arguments or not.
        /// </summary>
        public bool? IgnoreExtraArguments { get; set; }

        /// <summary>
        ///     Gets the aliases of the <see cref="Module"/>.
        /// </summary>
        public List<string> Aliases { get; }

        /// <summary>
        ///     Gets the checks of the <see cref="Module"/>.
        /// </summary>
        public List<CheckBaseAttribute> Checks { get; }

        /// <summary>
        ///     Gets the attributes of the <see cref="Module"/>.
        /// </summary>
        public List<Attribute> Attributes { get; }

        /// <summary>
        ///     Gets the commands of the <see cref="Module"/>.
        /// </summary>
        public List<CommandBuilder> Commands { get; }

        /// <summary>
        ///     Gets the submodules of the <see cref="Module"/>.
        /// </summary>
        public List<ModuleBuilder> Submodules { get; }

        internal Type Type { get; }

        /// <summary>
        ///     Initialises a new <see cref="ModuleBuilder"/>.
        /// </summary>
        public ModuleBuilder()
        {
            Aliases = new List<string>();
            Checks = new List<CheckBaseAttribute>();
            Attributes = new List<Attribute>();
            Commands = new List<CommandBuilder>();
            Submodules = new List<ModuleBuilder>();
        }

        internal ModuleBuilder(Type type) : this()
            => Type = type;

        /// <summary>
        ///     Sets the <see cref="Name"/>.
        /// </summary>
        public ModuleBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="Description"/>.
        /// </summary>
        public ModuleBuilder WithDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="RunMode"/>.
        /// </summary>
        public ModuleBuilder WithRunMode(RunMode? runMode)
        {
            RunMode = runMode;
            return this;
        }

        /// <summary>
        ///     Sets the <see cref="IgnoreExtraArguments"/>.
        /// </summary>
        public ModuleBuilder WithIgnoreExtraArguments(bool? ignoreExtraArguments)
        {
            IgnoreExtraArguments = ignoreExtraArguments;
            return this;
        }

        /// <summary>
        ///     Adds aliases to <see cref="Aliases"/>.
        /// </summary>
        public ModuleBuilder AddAliases(IEnumerable<string> aliases)
        {
            foreach (var alias in aliases)
                if (!string.IsNullOrWhiteSpace(alias) && !Aliases.Contains(alias))
                    Aliases.Add(alias.Trim());

            return this;
        }

        /// <summary>
        ///     Adds aliases to <see cref="Aliases"/>.
        /// </summary>
        public ModuleBuilder AddAliases(params string[] aliases)
        {
            for (var i = 0; i < aliases.Length; i++)
                if (!string.IsNullOrWhiteSpace(aliases[i]) && !Aliases.Contains(aliases[i]))
                    Aliases.Add(aliases[i].Trim());

            return this;
        }

        /// <summary>
        ///     Adds a check to <see cref="Checks"/>.
        /// </summary>
        public ModuleBuilder AddCheck(CheckBaseAttribute check)
        {
            Checks.Add(check);
            return this;
        }

        /// <summary>
        ///     Adds checks to <see cref="Checks"/>.
        /// </summary>
        public ModuleBuilder AddChecks(params CheckBaseAttribute[] checks)
        {
            Checks.AddRange(checks);
            return this;
        }

        /// <summary>
        ///     Adds an attribute to <see cref="Attributes"/>.
        /// </summary>
        public ModuleBuilder AddAttribute(Attribute attribute)
        {
            Attributes.Add(attribute);
            return this;
        }

        /// <summary>
        ///     Adds attributes to <see cref="Attributes"/>.
        /// </summary>
        public ModuleBuilder AddAttributes(params Attribute[] attributes)
        {
            Attributes.AddRange(attributes);
            return this;
        }

        /// <summary>
        ///     Adds a command to <see cref="Commands"/>.
        /// </summary>
        public ModuleBuilder AddCommand(CommandBuilder commandBuilder)
        {
            Commands.Add(commandBuilder);
            return this;
        }

        /// <summary>
        ///     Adds commands to <see cref="Commands"/>.
        /// </summary>
        public ModuleBuilder AddCommands(params CommandBuilder[] commandBuilders)
        {
            Commands.AddRange(commandBuilders);
            return this;
        }

        /// <summary>
        ///     Adds a submodule to <see cref="Submodules"/>.
        /// </summary>
        public ModuleBuilder AddSubmodule(ModuleBuilder moduleBuilder)
        {
            Submodules.Add(moduleBuilder);
            return this;
        }

        /// <summary>
        ///     Adds submodules to <see cref="Submodules"/>.
        /// </summary>
        public ModuleBuilder AddSubmodules(params ModuleBuilder[] moduleBuilders)
        {
            Submodules.AddRange(moduleBuilders);
            return this;
        }

        internal Module Build(CommandService service, Module parent)
        {
            if (Submodules.Count == 0 && Commands.Count == 0)
                throw new InvalidOperationException("Command modules must have at least one submodule or command.");

            return new Module(service, this, parent);
        }
    }
}