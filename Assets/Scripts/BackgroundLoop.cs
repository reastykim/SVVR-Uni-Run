using UnityEngine;

// 왼쪽 끝으로 이동한 배경을 오른쪽 끝으로 재배치하는 스크립트
public class BackgroundLoop : MonoBehaviour {
    private float width; // 배경의 가로 길이

    private void Awake() {
        // 가로 길이를 측정하는 처리
        // 자신의 박스 콜라이더 2D 컴포넌트로 접근하여 size 필드의 x값 가져오기
        width = GetComponent<BoxCollider2D>().size.x;
    }

    private void Update() {
        // 현재 위치가 원점에서 왼쪽으로 width 이상 이동했을때 위치를 리셋
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    // 위치를 리셋하는 메서드
    private void Reposition() {
        //transform.position = transform.position + Vector3.right * width * 2;
        // 오른쪽으로 가로길이 두배만큼 밀기
        transform.position += Vector3.right * width * 2;
    }
}