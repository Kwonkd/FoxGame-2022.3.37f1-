using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject exitUI;
    public bool toggleState = false;
    public Button[] stageButtons; // 스테이지 버튼들을 담을 배열
    public string[] stageSceneNames; // 버튼에 연결될 씬 이름 배열

    private int currentIndex = 0; // private으로 변경하여 외부 수정을 방지

    void Start()
    {
        // 배열이 비어있거나 크기가 맞지 않으면 경고 출력
        if (stageButtons.Length == 0 || stageButtons.Length != stageSceneNames.Length)
        {
            Debug.LogWarning("StageButtons 또는 SceneNames가 설정되지 않았거나 크기가 일치하지 않습니다.");
            return;
        }

        // 각 버튼에 클릭 이벤트 동적 할당
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int index = i; // 클로저 문제 방지를 위해 지역 변수에 인덱스 복사
            stageButtons[i].onClick.AddListener(() => LoadStage(index));
        }

        // 초기 선택 버튼 설정
        stageButtons[currentIndex].Select();
    }

    void Update()
    {
        // 방향키 입력 감지
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }

        // 엔터 또는 스페이스 키로 스테이지 로드
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            // 현재 선택된 버튼의 클릭 이벤트를 실행하는 것이 더 안정적
            stageButtons[currentIndex].onClick.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleExitUI();
        }
    }

    private void ChangeSelection(int direction)
    {
        // 다음 인덱스 계산
        currentIndex += direction;

        // 인덱스가 배열 범위를 벗어나지 않도록 조정 (순환 구조)
        if (currentIndex < 0)
        {
            currentIndex = stageButtons.Length - 1;
        }
        else if (currentIndex >= stageButtons.Length)
        {
            currentIndex = 0;
        }

        // 새로운 버튼 선택
        stageButtons[currentIndex].Select();
    }

    public void LoadStage(int stageIndex)
    {
        // 유효한 인덱스인지 확인
        if (stageIndex < 0 || stageIndex >= stageSceneNames.Length)
        {
            Debug.LogError("잘못된 스테이지 인덱스입니다: " + stageIndex);
            return;
        }

        Debug.Log(stageSceneNames[stageIndex] + " 씬을 로드합니다.");
        SceneManager.LoadScene(stageSceneNames[stageIndex]);
    }

    public void OnClickCancel ()
    {
        exitUI.SetActive(false);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
    void ToggleExitUI()
    {
        toggleState = !toggleState;
        if (exitUI != null)
        {
            exitUI.SetActive(toggleState); // 상태에 따라 오브젝트 활성화/비활성화
        }
    }
}