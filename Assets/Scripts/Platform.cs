using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
        // 밟힘 상태를 리셋
        stepped = false;
        
        // 장애물의 갯수만큼 순서대로 루프
        for(int i = 0; i< obstacles.Length; i++)
        {
            // 현재 순번의 장애물을 1/3 확률로 활성화
            if(Random.Range(0,3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    // void OnDisable() // 컴포넌트가 비활성화될때 자동 실행

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
        // 충돌한 상대방 태그가 Player && 아직 안 밟힘
        if(collision.collider.tag == "Player" && !stepped)
        {
            // 밟힘상태를 참으로 전환후, 점수 증가
            stepped = true;
            // 점수 증가 실행
            GameManager.instance.AddScore(1);
        }
    }
}