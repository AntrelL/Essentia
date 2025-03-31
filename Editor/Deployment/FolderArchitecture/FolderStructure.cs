using UnityEngine;

namespace Essentia.Deployment.Editor
{
	[CreateAssetMenu(fileName = "FolderStructure", menuName = "Scriptable Objects/Folder Structure")]
	public class FolderStructure : ScriptableObject
	{
		[field: SerializeField] public string[] Folders { get; private set; }
	}
}
