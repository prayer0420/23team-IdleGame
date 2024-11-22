## 🎤프로젝트 소개
플레이어가 스스로 사냥하는 방치형 게임! 아이템을 조합해 최강의 캐릭터를 만들자!  <br>
<img width = 400px src = "https://github.com/user-attachments/assets/7a149509-a6d3-408d-997b-eaf157fd5b14"><br>

## 👨‍👨‍👦 팀 구성 및 역할 소개
| 역할   | 이름     | 담당 업무 |
|--------|----------|-----------------------------------------|
| 팀장   | 박기도   | 맵&스테이지 구성, 저장 및 오디오시스템      |
| 팀원   | 박찬형   | 인벤토리, 아이템, 아이템 뽑기 시스템        |
| 팀원   | 김경구   | 플레이어 및 적 AI 구현, 전투 시스템        |
| 팀원   | 이승희   |                                          |

## 개발기간
2024.11.15 ~ 2024.11.22

## 주요기능
> ## 플레이어 및 적 유한 상태머신
> - **플레이어의 상태 관리**: 플레이어의 이동 및 공격 상태를 관리하고, 플레이어가 특정 조건을 만족하면 상태가 전환되도록 처리합니다.
> - **몬스터의 상태 관리**: 몬스터의 이동, 일반 공격, 독 공격, 스턴(넉백) 공격을 포함한 다양한 공격 및 상태 전환을 처리합니다.
>
>   ### 세부 내용
> - **플레이어 이동 상태**: 플레이어는 시작 시 특정 위치로 이동하고, 이후 위치가 변경되더라도 항상 시작 위치로 돌아오는 이동 패턴을 FSM으로 처리했습니다.
- **플레이어 공격 상태**: 몬스터가 플레이어의 공격 범위 안에 들어오면 플레이어가 자동으로 공격하는 로직을 FSM에 포함시켰습니다.
- **몬스터 이동 상태**: 몬스터는 플레이어를 타겟으로 삼아 이동하며, 플레이어의 위치에 맞춰 방향을 변경하고 이동하는 로직을 FSM에 반영했습니다.
- **몬스터 공격 상태**: 몬스터는 일반 공격, 독 공격, 스턴(넉백) 공격을 FSM으로 처리합니다.
  - **일반 공격**: 일정 범위 내에서 플레이어에게 데미지를 주는 공격.
  - **독 공격**: 독 공격에 피격 시, 일정 시간 동안 지속적으로 데미지를 입는 공격.
  - **스턴(넉백) 공격**: 피격 시 플레이어가 밀려나가는 공격.
 
    ### 상세한 설명
1. **플레이어 이동 로직**: 
   - 플레이어는 **시작 위치**로 이동하는 초기 상태를 가지고 있으며, 이후 **다른 위치로 이동**하더라도 항상 시작 위치로 돌아오는 특성을 가지고 있습니다. 이를 FSM 상태 전환으로 관리하고 있습니다.
   
2. **플레이어 공격 로직**: 
   - 플레이어가 **몬스터가 공격 범위 안에 들어왔을 때** 자동으로 공격하는 기능을 구현했습니다. 이를 위해 공격 범위와 타겟 몬스터를 체크하여 공격 상태를 전환합니다.

3. **몬스터 이동 로직**:
   - 몬스터는 **플레이어를 타겟**으로 삼고 **플레이어의 위치를 따라 이동**합니다. 플레이어의 위치가 변하면 몬스터의 이동 경로와 방향도 그에 맞춰 동적으로 변화합니다.

4. **몬스터 공격 로직**:
   - **일반 공격**: 몬스터가 플레이어와 일정 범위 내에 있을 때, 단순히 데미지를 입히는 공격을 합니다.
   - **독 공격**: 독 공격을 받으면 **지속적인 데미지**가 일정 시간 동안 발생하도록 구현했습니다. 독 공격이 활성화되면 일정 시간마다 반복적으로 데미지가 적용됩니다.
   - **스턴(넉백) 공격**: 몬스터의 스턴 공격은 피격 시 **플레이어가 밀려나가게** 만드는 로직으로, 플레이어의 위치를 뒤로 밀어내는 효과를 FSM을 통해 처리합니다.


> ## 아이템 가챠 시스템
> **아이템 뽑기 / 능력치 확인**<br>
> <img width = 350px src = "https://github.com/user-attachments/assets/c82daa84-6b26-4459-9263-303a9b6c8483"><br>
> **아이템 뽑기 / 능력치 비교, 교체**<br>
> <img width = 350px src = "https://github.com/user-attachments/assets/003865f4-1ce6-4b28-827f-11853e679bce"><br>



> ## 맵 및 스테이지 구성
> **스테이지 구성**<br>
> 난이도 : 일반, 어려움 / 일반 모드 클리어시 어려움 모드 해금<br>
> 챕터 : 1, 2, 3 / 챕터 별 맵과 나타나는 적이 다름<br>
> 스테이지 : 1, 2, 3 / 1~2스테이지는 일반몬스터, 3스테이지에서는 보스몬스터 출현 <br><br>
> 스테이지에 출현하는 몬스터를 모두 처치하면 다음 스테이지 이동 가능!<br>
> <img width = 300px src = "https://github.com/user-attachments/assets/75beb3f5-bd09-407d-bdba-722180ade2e9"><br><br>
> 이미 클리어한 스테이지 및 챕터는 가능<br>
> <img width = 300px src = "https://github.com/user-attachments/assets/c5cec43a-816f-47af-8629-98079093cd17"><br><br>
> <br>

> ## 리소스 및 오브젝트 관리
> **리소스 매니저 및 오브젝트 풀링** <br>
> 중복 코드 개선, 중앙 관리 가능, 메모리 효율 증대 <br>
> **맵 및 오디오 생성 코드**<br>
> <img width = 530px src = "https://github.com/user-attachments/assets/be105232-cca1-40be-a44e-6676ba81314a"> <img width = 430px src = "https://github.com/user-attachments/assets/25c43dcb-ee31-435b-8691-7d068086a4e0"><br>


> ## 세이브 및 오디오 시스템
> * 데이터를 저장 및 불러올 수 있습니다.
> * 오디오의 설정을 변경할 수 있습니다.
>   
## 기타
* Version: Unity 2022.03.17f
