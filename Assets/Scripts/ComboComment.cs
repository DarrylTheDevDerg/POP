using System;
using TMPro;
using UnityEngine;

[Serializable]
class CommentThreshold
{
    [Range(0, int.MaxValue)]
    public int greaterThan, lessThan;
    public string comment;
}

public class ComboComment : MonoBehaviour
{
    public TextMeshProUGUI commentText;
    
    [SerializeField] CommentThreshold[] comments;

    private float _innerTimer;
    private PlayerManager _playerManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerManager = FindFirstObjectByType(typeof(PlayerManager)) as PlayerManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
