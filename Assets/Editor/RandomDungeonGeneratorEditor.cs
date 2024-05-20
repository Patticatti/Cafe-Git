using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AbstractMapGenerator), true)] //if true,child classes will also show editor
public class RandomDungeonGeneratorEditor : Editor //inherit from editor
{
    AbstractMapGenerator generator;

    private void Awake()
    {
        generator = (AbstractMapGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Dungeon")) //create custom button in inspector, only works if clicked

        {
            generator.GenerateDungeon();
        }
    }

}
