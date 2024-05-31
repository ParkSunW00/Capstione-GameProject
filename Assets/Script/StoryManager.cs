using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    private static StoryManager instance; // instance 변수를 static으로 변경

    public PlayerData playerData;
    public DataManager dataManager;
    public ChatPrint chatPrint;
    public GameObject player;
    public GameObject littlePrince;
    public GameObject questInfo;
    
    private int mainQuestNum = 0;
    public int dialogueGroupId = 0;

    void Awake()
    {
        player = GameObject.Find("rose");
        littlePrince = GameObject.Find("어린 왕자");
        questInfo = GameObject.Find("MainScreen").transform.Find("QuestInfo").gameObject;
        playerData = GameObject.Find("DataManager").GetComponent<PlayerData>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        chatPrint = GameObject.Find("Chat").GetComponent<ChatPrint>();
        
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // 스토리 루틴 시작
        StartCoroutine(StoryRoutine());
    }

    private IEnumerator StoryRoutine()
    {
        // 게임 시작
        while (true)
        {
            // 초반 대화를 순서대로 끝마치는 경우 메인퀘스트 1번 얻음
            yield return StartCoroutine(CheckInitialConversations(1));
            
            // 어린왕자와 대화 이후 5초 대기
            yield return new WaitForSeconds(5f);
            
            // 나레이션 나온다
            ChatSelfStart("나레이션", 2);
            
            // 다음 어린왕자와 대화함
            questInfo.GetComponent<TextMeshProUGUI>().text = "퀘스트 정보\n- 어린왕자와 대화하기";
            yield return StartCoroutine(CheckInitialConversations(3));
            
            // 나레이션 으로 서브퀘스트 받고
            ChatSelfStart("나레이션", 4);
            questInfo.GetComponent<TextMeshProUGUI>().text = "퀘스트 정보\n- 놀이터로 이동하기";
            
            // 놀이터 가는 시간 기다리기
            // 어린왕자 놀이터로 이동시킴
            // 10초 후 자동 이동
            // 현재 위치가 놀이터인지 판단하기
            yield return StartCoroutine(CheckNowPlace("playground"));
            Debug.Log("현재 장소 놀이터!");
            questInfo.GetComponent<TextMeshProUGUI>().text = "퀘스트 정보\n- 놀이터에서 어린왕자 찾기";
            ChatSelfStart("나레이션", 5);
            
            // 얘가 진짜 어디갔지?
            yield return new WaitForSeconds(10f);
            ChatSelfStart("장미", 6);
            
            // 모래성 차는 동작
            yield return new WaitForSeconds(10f);
            ChatSelfStart("장미", 7);
            
            // 여우 나오고 도망치게 합니다
            yield return new WaitForSeconds(10f);
            ChatSelfStart("장미", 8);
            
            // #2-6: Day+5, 집 앞
            // 집앞으로 이동하게 합니다
            // 집 완성시 캐릭터 좌표 이동하거나 신전환하여 집앞이나 집으로 이동시킵니다
            questInfo.GetComponent<TextMeshProUGUI>().text = "퀘스트 정보\n- 장미의 집앞으로 이동하기";
            yield return new WaitForSeconds(20f);
            ChatSelfStart("장미", 9);
            
            // #2-7: Day+6, 놀이터
            questInfo.GetComponent<TextMeshProUGUI>().text = "퀘스트 정보\n- 다시 놀이터로 가보기";
            yield return new WaitForSeconds(20f);
            player.transform.position = new Vector3(80.07f, 0, -26.16f);
            ChatSelfStart("장미",10);
            
            
            Debug.Log("프롤로그 종료");
            mainQuestNum++;
            GetMainQuest(mainQuestNum);
            Debug.Log("메인퀘스트 #1 획득 완료!!");
            
            
            // 이후 로직 작성
            break;
        }
    }

    private void ChatSelfStart(string npcName, int id)
    {
        dialogueGroupId = id;
        chatPrint.ChatOpen(npcName);
    }

    // 대화 횟수 체크해서 퀘스트 완료시킵니다
    private IEnumerator CheckInitialConversations(int i)
    {
        while (playerData.talkCount < i)
        {
            yield return null; // Wait until talkCount reaches i
        }
    }

    private IEnumerator CheckNowPlace(string placeName)
    {
        while (playerData.nowPlace != placeName)
        {
            yield return null;      // 지정 장소로 이동할 때까지 대기
        }
    }

    private void GetMainQuest(int questNum)
    {
        QuestData questData = dataManager.questDataManager.questDataList[questNum - 1];
        playerData.PlayerQuestList.Add(questData);
        Debug.Log("메인퀘스트 #1 리스트 저장 완료!!");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
