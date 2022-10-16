using System;
using System.Linq;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

public static class Patcher
{
    public static IEnumerable<string> TargetDLLs { get; } = new[] { "Assembly-CSharp.dll" };

    public static ModuleDefinition Plugin;

    public static void Patch(AssemblyDefinition assembly) {
        // open plugin definition
        Plugin = ModuleDefinition.ReadModule("BepInEx/plugins/MonoArk.dll", new ReaderParameters { ReadWrite = false });

        foreach (var module in assembly.Modules)
        {
            foreach (var type in module.Types)
            {
                if (type.FullName == "TempSaveData")
                {
                    AddExtensions(module, type);
                }
            }
        }

        // cleanup plugin
        Plugin.Dispose();
    }

    public static void AddExtensions(ModuleDefinition module, TypeDefinition ty) {
        var extensionTy = Plugin.GetType("MonoArk.Core.ModExtensions");
        var fieldDef = new FieldDefinition(
            "Extensions",
            FieldAttributes.Public,
            module.ImportReference(extensionTy));
        ty.Fields.Add(fieldDef);

        // add initialization to constructor
        var ctorExtension = extensionTy.Methods.First(method => method.Name == ".ctor" && method.Parameters.Count == 0);

        var ctorDef = ty.Methods.First(method => method.Name == ".ctor");
        var instructions = ctorDef.Body.Instructions;

        instructions.Insert(0, Instruction.Create(OpCodes.Ldarg_0));
        instructions.Insert(1, Instruction.Create(OpCodes.Newobj, module.ImportReference(ctorExtension)));
        instructions.Insert(2, Instruction.Create(OpCodes.Stfld, fieldDef));
    }
}
