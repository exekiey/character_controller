using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(PlayerStats))]
public class StatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerStats playerStats = (PlayerStats)target;

        if (playerStats.AutomaticCalculateJumpVariables)
        {


            playerStats.JumpHeight = EditorGUILayout.FloatField(new GUIContent("Jump Height"), playerStats.JumpHeight);
            playerStats.JumpTime = EditorGUILayout.FloatField(new GUIContent("Jump Time"), playerStats.JumpTime);
            playerStats.JumpInitialVelocity = (2 * playerStats.JumpHeight) / playerStats.JumpTime;
            playerStats.JumpGravity = (-2 * playerStats.JumpHeight) / (playerStats.JumpTime * playerStats.JumpTime);

        }

        if (playerStats.AutomaticCalculateWalkVariables)
        {


            playerStats.TimeToReachMaxVelocity = EditorGUILayout.FloatField(new GUIContent("Time to reach max velocity"), playerStats.TimeToReachMaxVelocity);
            playerStats.WalkAcceleration = playerStats.WalkMaxVelocity / playerStats.TimeToReachMaxVelocity;

        }

        if (GUILayout.Button("Save to JSON"))
        {

            string stats = JsonUtility.ToJson(playerStats);
            System.IO.File.WriteAllText("Assets/PlayerStats/playerStats.json", stats);

        }


        if (GUILayout.Button("Load from JSON"))
        {
            string stats = File.ReadAllText("Assets/PlayerStats/playerStats.json");
            JsonUtility.FromJsonOverwrite(stats, playerStats);
        }
    }
}