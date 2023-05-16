using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBox: MonoBehaviour
{
    public Dictionary<int, string[]> talkData;
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        //루루 대화
        talkData.Add(0, new string[] { "잉? 왜 거기서 나와? 포탈의 출구는 내 뒤에 있는 분수대 앞인데...?", "아무튼 지금 마을상황이 안좋아...", 
            "요새 무슨일인지 몬스터들이 많이 난폭해졌어", "엘폰 숲 입구의 슬라임을 3마리 정도 퇴치해줘","나는 몬스터들이 난폭해진 이유를 찾고 있을께" });
        talkData.Add(1, new string[] { "도와줘! 마을 사람들을 위험하게 둘수 없어!" });
        talkData.Add(2, new string[] { "고마워! 보상으로 경험치와 돈을 좀 줄께","너가 슬라임을 퇴치하는 중에 알아봤는데","엘폰 숲 깊은곳에 숲의 주인이 난동을 부리고 있더라고",
            "가만히 둘수 없어 숲의 주인을 퇴치해줘!","만만치 않은 상대일꺼야 준비 단단히 하고 출발해!"});
        talkData.Add(3, new string[] { "준비 단단히 해!" });
        talkData.Add(4, new string[] { "와! 숲의 주인을 물리치다니...","역시 용사야! 대단해",
            "근데 숲의 주인이 없는데도 아직도 몬스터가 난폭해...","난 더 조사해볼테니 너는 더 실력을 키우도록해!","그럼 나중에봐!"});
        talkData.Add(5, new string[] { "나중에 봐!" });

        //마법사 대화
        talkData.Add(100, new string[] { "허허... 이 늙은이에게 무슨 볼일인가?" });

        //딸기 대화
        talkData.Add(200, new string[] { "어머! 용사님 안녕하세요?" });

        //숭아 대화
        talkData.Add(300, new string[] { "요즘 피부가 까칠해..." });

        //카도 대화
        talkData.Add(400, new string[] { "건드리지 마세요! 낚시중이에요!" });

        //요정 대화
        talkData.Add(500, new string[] { "아... 어떻게 해야하지?" });

        //던전 관리인
        talkData.Add(600, new string[] { "이곳은 엘폰 숲 입니다!" });
    }

    public string GetTalk(int count, int talkIndex)
    {
        if (talkIndex == talkData[count].Length)
        {
            return null;
        }
        else
            return talkData[count][talkIndex];
    }
}
