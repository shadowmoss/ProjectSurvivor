using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectSurvivor {
    public class Enemy : ProjectSurvivorController
    {
        public float movementSpeed = 2.0f;
        private SpriteRenderer sprite;
        private void Start()
        {
            sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            Player player =  FindObjectOfType<Player>();
            // 计算当前位置与player位置的向量
            Vector2 direction = (player.transform.position - transform.position).normalized;
            // 将自身向player移动
            transform.Translate(direction * Time.deltaTime * movementSpeed);
        }
    }
}

