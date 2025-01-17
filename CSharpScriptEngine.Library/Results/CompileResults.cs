﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using CSharpScriptEngine.Library.Options;
using Microsoft.CodeAnalysis;

namespace CSharpScriptEngine.Library.Results
{
    public class CompileResults
    {
        private readonly Diagnostic[]? _diagnostics;

        public CompileResults(bool isSuccessful,
            CompileOptions compileOptions,
            byte[]? pdbBytes = null,
            byte[]? peBytes = null,
            ImmutableArray<Diagnostic>? diagnostics = default)
        {
            PdbBytes = pdbBytes;
            PeBytes = peBytes;
            IsSuccessful = isSuccessful;
            _diagnostics = diagnostics?.ToArray();
            CompileOptions = compileOptions;
        }

        public Assembly? Assembly =>
            IsSuccessful switch
            {
                true => Assembly.Load(PeBytes!, PdbBytes!),
                false => default
            };

        public CompileOptions CompileOptions { get; }
        public IEnumerable<Diagnostic>? Diagnostics => _diagnostics;

        public bool ExistDiagnostics =>
            _diagnostics switch
            {
                {Length: > 0} => true,
                _ => false
            };

        public byte[]? PdbBytes { get; }
        public byte[]? PeBytes { get; }
        public bool IsSuccessful { get; }
    }
}
