using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button[] stageButtons; // 스테이지 버튼들을 담을 배열
    public int currentIndex = 0;

    void Start()
    {
        // 초기 선택 버튼 설정
        if (stageButtons.Length > 0)
        {
            stageButtons[0].Select();
        }
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadStage(currentIndex);
        }
    }

    private void ChangeSelection(int direction)
    {
        // 현재 선택된 버튼의 인덱스 찾기
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == stageButtons[i].gameObject)
            {
                currentIndex = i;
                break;
            }
        }

        // 다음 인덱스 계산
        currentIndex += direction;

        // 인덱스가 배열 범위를 벗어나지 않도록 조정
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
    public void LoadStage(int stageNumber)
    {
        Debug.Log("스테이지 " + (stageNumber + 1) + " 로딩");
        switch(stageNumber)
        {
            case 0:
                SceneManager.LoadScene("Stage 1");
                break;
            case 1:
                SceneManager.LoadScene("Stage 2");
                break;
            case 2:
                SceneManager.LoadScene("Stage 3");
                break;
        }
    }
}