using Essentia.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Essentia.Infrastructure
{
    public class EntryPoint : MonoBehaviour
    {
		private const string MissingMainGameTypeError = "Main game type not found.";
		private const string MultipleMainGameTypesError = "Only one type should inherit from type " + nameof(Program) + ".";
		private const string MissingEntryPointMethodError = "Entry point method not found.";

		private void Awake()
		{
			List<Type> derivedTypes = GetDerivedTypes(typeof(Program));

			if (VerifyNumberOfMainGameTypes(derivedTypes) == false)
				return;

			if (TryGetEntryPointMethod(derivedTypes[0], out MethodInfo entryPointMethod) == false)
				return;

			entryPointMethod.Invoke(null, null);	
		}

		private List<Type> GetDerivedTypes(Type baseType)
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type => 
					type.IsClass &&
					type.IsAbstract == false &&
					baseType.IsAssignableFrom(type))
				.ToList();
		}

		private bool VerifyNumberOfMainGameTypes(List<Type> derivedTypes)
		{
			if (derivedTypes.Count == 0)
			{
				Console.LogError<EntryPoint>(MissingMainGameTypeError);
				return false;
			}

			if (derivedTypes.Count > 1)
			{
				Console.LogError<EntryPoint>(MultipleMainGameTypesError);
				return false;
			}

			return true;
		}

		private bool TryGetEntryPointMethod(Type gameType, out MethodInfo entryPointMethod)
		{
			entryPointMethod = gameType.GetMethod(
				Metadata.EntryPointMethodName,
				Metadata.EntryPointMethodBindingFlags);

			if (entryPointMethod is null || entryPointMethod.GetParameters().Length != 0)
			{
				Console.LogError<EntryPoint>(MissingEntryPointMethodError);
				return false;
			}

			return true;
		}
	}
}
