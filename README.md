# Unity_3D_Portfolio
![Unity](https://img.shields.io/badge/Unity-2022.3-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?logo=c-sharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-PC-blue)
<!---
## 0.목차
-[프로젝트 소개](#1.-프로젝트-소개)
 -[개발 기간](#개발-기간)

-[핵심 기능](#핵심-기능)

-[기술 스택](#기술-스택)

-[문제 상황 및 해결 방법](#문제-상황-및-해결-방법) --->

## 1. 프로젝트 소개
<div align="center">
  <img src="https://github.com/user-attachments/assets/51a7a697-c9a7-4f9c-99ea-6034b06b728b" width="100%">
<br>
<게임 메인 이미지>

<br>
<br>
<div align="left">
  
●  Unity 3D 경영 시뮬레이터 게임입니다.
<br>
●  대학 진학 중 프로그래머로 전향 후 최초로 제작한 게임입니다.
<br>
●  본 Repository에는 저작권 문제로 인해 코드만 업로드 되어 있습니다.
<br>
<br>
<table align="center">
  <tr>
    <td><img src="https://github.com/user-attachments/assets/65bc92ba-d192-461a-9908-10b2fa3f4f97" width="400" height="250"></td>
    <td><img src="https://github.com/user-attachments/assets/deb907b9-6a72-4bf9-a2a1-5fa7de03256c" width="400" height="250"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/a7180180-1493-4aaa-978e-0dc63d738244" width="400" height="250"></td>
    <td><img src="https://github.com/user-attachments/assets/bac041c8-5f9c-4a94-a2ed-351c309469e0" width="400" height="250"></td>
  </tr>
</table>
<div align="center">
<인게임 이미지>
<br>
<div align="left">
이승과 저승 사이에 존재하는 가상의 숲을 배경으로 하는 3D 경영 시뮬레이터 게임입니다.

3일간 가게를 운영하며 재료를 주문하고, 미니 게임을 통해 음식을 만들어 손님들에게 대접하여 각 일차의 마지막에 스토리를 진행하는 특별 NPC의 주문을 받게 됩니다. 

## 2. 개발 환경
<br>
● 2022.3.38f1
<br>
● C#
<br>
● Visual Studio Code

## 3. 사용 기술
| 기술 | 설명 |
| --- | --- |
| 디자인 패턴 | Singleton 패턴과 상속을 이용한 코드 개선 <br> 중앙 집중식 데이터 관리(NPCManager) 구현 |
| 자원 관리 | Boolean Array 기반의 좌석 자원 할당 알고리즘, 단일 진입점을 통한 예약/반환 관리 |
| 물리 충돌 | 여러 종류의 Collider를 접목한 물리 충돌 최적화 <br> RayCast를 활용한 오브젝트 인터랙션 감지 |
| 애니메이션 | Unity Animator를 활용한 애니메이션과 물리적 충돌 동기화 |
| 이벤트 처리 | Delegate와 Event를 활용한 아이템 파괴 콜백 처리 |
| 씬/데이터 관리 | DontDestroyOnLoad와 Singleton을 결합한 씬 간 데이터 유지 |
| 카메라 | 5개 카메라 전환을 통한 미니게임·UI 전환 시스템 구현 |
| 에셋 연동 | Blender 및 3D Max 툴 내의 에셋 연동, Unity 에셋을 활용한 오브젝트 Outline 구현 |

<br>

## 4. 조작 방법

| 입력 | 동작 |
| --- | --- |
| W / A / S / D | 이동 |
| 마우스 | 시점 회전 |
| Left Shift | 달리기 |
| Space | 점프 |
| E | 상호작용 |
| 마우스 좌클릭 | 아이템 소환 / 상호작용 |
| Q | 구매(레시피) UI 열기/닫기 |
| Tab | 접시 활성화 |
| Esc | 설정 메뉴 |
<br>


## 5. 트러블슈팅

>**카페 좌석 중복 예약 및 미반환 문제**

● 문제 상황
<br>
여러 NPC가 거의 동시에 빈 좌석을 탐색할 경우, 각 NPC 스크립트가 좌석 배열을 직접 읽고 쓰면서 같은 인덱스를 중복 예약하는 문제 발생. NPC가 파괴된 이후에도 자리가 점유 상태로 남아 빈 자리가 계속 줄어드는 현상도 함께 발생.

```csharp
// 기존 — 각 NPC가 배열을 직접 탐색/점유
for (int i = 0; i < NPCManager.Instance.isNPCInPlace.Length; i++)
{
    if (!NPCManager.Instance.isNPCInPlace[i])
    {
        availableIndex = i;
        NPCManager.Instance.isNPCInPlace[i] = true;
        break;
    }
}
```

● 원인 추적 및 해결 방법
<br>
좌석이라는 자원에 여러 NPC 스크립트가 각자의 방식으로 접근하고 있다는 공통 원인 발견. NPCManager를 좌석 예약의 단일 진입점으로 만들어 모든 NPC가 `ReserveSpot()` / `ReleaseSpot()`을 통해서만 좌석에 접근하도록 재설계함.

```csharp
// 수정 — NPCManager를 통한 단일 진입점 예약/반환
public int ReserveSpot()
{
    if (isCafeFull) return -1;

    for (int i = 0; i < isNPCInPlace.Length; i++)
    {
        if (!isNPCInPlace[i])
        {
            isNPCInPlace[i] = true;
            UpdateCafeStatus();
            return i;
        }
    }
    return -1;
}

public void ReleaseSpot(int index)
{
    if (index < 0 || index >= isNPCInPlace.Length) return;
    if (isNPCInPlace[index])
    {
        isNPCInPlace[index] = false;
        UpdateCafeStatus();
    }
}
```

NPC가 파괴될 때 `OnDestroy()`에서 `ReleaseSpot()`을 자동 호출하도록 구현.

```csharp
void OnDestroy()
{
    if (reservedIndex != -1)
    {
        NPCManager.Instance.ReleaseSpot(reservedIndex);
        reservedIndex = -1;
    }
}
```

● 결과
<br>
좌석 중복 예약 및 미반환 문제 재현되지 않음. 좌석 관련 로직이 NPCManager 한 곳으로 집중되어, NPC 종류가 늘어나도 예약 로직을 다시 구현할 필요 없음.

<br>

>**카페 오픈/마감 토글 시 실제 입장 가능 상태가 즉시 반영되지 않는 문제**

● 문제 상황
<br>
카페의 "영업중/마감" 간판 UI는 `CafeOpenObject.test`가 관리하고, NPC가 실제로 들어올 수 있는지는 `NPCTrigger.NPCComeToCafe`가 완전히 다른 스크립트에서 따로 관리. 플레이어가 간판과 상호작용하면 두 값을 각각 직접 반전시키는 구조였음.

```csharp
// 기존 — 간판 상호작용 시 두 스크립트의 상태를 직접 각각 반전
cafeOpen.test = !cafeOpen.test;
nPCTrigger.NPCComeToCafe = !nPCTrigger.NPCComeToCafe;
```

`NPCComeToCafe` 값만 반전시키고, 그 값을 실제 입장 판정에 쓰이는 트리거 콜라이더 태그(`CanGo`/`CantGo`)에 반영하는 로직은 호출되지 않아, 간판은 "영업중"으로 바뀌었는데 정작 입장 판정 태그는 다음 좌석 예약/반환 이벤트가 발생할 때까지 이전 상태로 남아있는 시간차 버그 발생.

● 원인 추적 및 해결 방법
<br>
상태값을 바꾸는 것과 그 값을 실제 판정 로직에 반영하는 것이 분리되어 있어, 호출하는 쪽에서 후자를 빼먹을 수 있는 구조라는 것이 원인. `NPCTrigger`에 전용 setter를 만들어 상태 변경과 판정 갱신을 하나로 묶음.

```csharp
// 수정 — 상태 변경과 판정 갱신을 하나의 메서드로 통합
public void SetNPCComeToCafe(bool value)
{
    NPCComeToCafe = value;
    UpdateTriggerState(NPCComeToCafe, currentTouchedNPCs >= maxNPCsAllowed);
}
```

```csharp
// PlayerItem.cs — 수정 코드
NPCTrigger trigger = FindObjectOfType<NPCTrigger>();
if (trigger != null)
{
    bool newState = !trigger.NPCComeToCafe;
    trigger.SetNPCComeToCafe(newState); // 상태 변경 + 태그 갱신이 한 번에 처리됨
    cafeOpen.test = !cafeOpen.test;
}
```

● 결과
<br>
상태를 바꾸는 진입점을 하나로 강제한 뒤로 간판 표시와 실제 입장 판정이 어긋나는 현상 재현되지 않음.
<br>

