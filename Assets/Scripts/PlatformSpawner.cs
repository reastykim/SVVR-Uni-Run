﻿using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -20); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        // 사용할 발판들을 생성
        platforms = new GameObject[count]; // count 갯수만큼 방을 가진 배열 생성

        // count 만큼 순회하면서 새로운 발판 생성
        for(int i = 0; i < count; i++)
        {
            // platformPrefab을 원본으로 새 발판을 poolPosition에 생성
            // 생성한 발판을 배열에 할당
            platforms[i] = Instantiate(platformPrefab, poolPosition,
                Quaternion.identity);
        }
        
        // 마지막 배치 시점을 리셋
        lastSpawnTime = 0;
        timeBetSpawn = 0;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        // 게임 오버 상태에서는 동작하지 않는다
        if(GameManager.instance.isGameover)
        {
            return;
        }

        // 마지막 배치 시점에서 timeBetSpawn 이상의 시간이 흘렀으면
        // = 현재 시간이 최근 배치 시점 + timeBetSpawn 보다 크다
        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            // 최근 배치 시점을 현재 시간으로 갱신
            lastSpawnTime = Time.time;

            // 다음 배치까지의 시간 간격으로 랜덤 변경
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            // 배치할 높이를 랜덤 설정
            float yPos = Random.Range(yMin, yMax);

            // 현재 순번의 발판을 껐다 켜기로 리셋 (OnEnable()이 자동 실행됨)
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            // 현재 순번의 발판의 위치를 화면 오른쪽으로 옮기기
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            // 순번 넘기기
            currentIndex++;
            // 마지막 순번에 도달했다면 순번을 처음부터 다시 시작
            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}