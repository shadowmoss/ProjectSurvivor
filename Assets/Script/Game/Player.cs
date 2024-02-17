using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace ProjectSurvivor {
    public class Player : ProjectSurvivorController
    {
        public float movementSpeed = 5.0f;
        // 玩家形象
        SpriteRenderer sprite;

        // 刚体组件
        Rigidbody2D selfRigidbody;
        void Start()
        {
            sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            selfRigidbody = this.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // 组合成方向向量
            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            // 根据输入量进行移动
            selfRigidbody.velocity = direction * movementSpeed;
        }
    }
}

