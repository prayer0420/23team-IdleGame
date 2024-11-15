using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("Animation")]
    [field: SerializeField] AnimationData animationData;

    public Animator animator {  get; private set; }
    [field: SerializeField] public PlayerSO Data { get;  set; }

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
