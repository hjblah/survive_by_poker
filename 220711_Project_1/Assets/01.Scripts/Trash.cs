//////////[SerializeField] private Text currnet_SelectGroup_Txt;
//////////    [HideInInspector] public GameObject current_SelectGroup;
//////////    [HideInInspector] public Vector3 current_DestinatoinPose;

//////////    private float m_fOldToucDis = 0f;       // 터치 이전 거리를 저장합니다.
//////////    private float m_fFieldOfView = 60f;     // 카메라의 FieldOfView의 기본값을 60으로 정합니다.
//////////    public float speed = 10.0f;
//////////    private float temp_value;

//////////    private Vector2 nowPos, prePos;
//////////    private Vector2 movePosDiff;

//////////    public List<GameObject> teamParent = new List<GameObject>();
//////////    [SerializeField] private GameObject unitParent;
//////////    [SerializeField] private GameObject selectButton;
//////////    [SerializeField] private GameObject[] unit;

//////////    public void Update()
//////////    {
//////////        if (!EventSystem.current.IsPointerOverGameObject())
//////////        {
//////////            if (Input.GetMouseButtonDown(0))
//////////            {
//////////                Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//////////                Ray2D ray = new Ray2D(wp, Vector2.zero);
//////////                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

//////////                if (hit)
//////////                {
//////////                    //현재 클릭된 그룹이 있을 떄 
//////////                    if (current_SelectGroup != null)
//////////                    {
//////////                        if (hit.transform.name == "Background")
//////////                        {
//////////                            current_DestinatoinPose = new Vector3(hit.point.x, hit.point.y, 0);
//////////                            selectButton.transform.position = current_DestinatoinPose;
//////////                            selectButton.SetActive(true);
//////////                        }
//////////                        //다시 클릭 시 현재 그룹 제거
//////////                        else if (hit.transform.name.Contains(current_SelectGroup.transform.name))
//////////                        {
//////////                            currnet_SelectGroup_Txt.text = "";
//////////                            current_SelectGroup = null;
//////////                            selectButton.SetActive(false);

//////////                            Time.timeScale = 1f;
//////////                        }

//////////                        if (selectButton.activeSelf)
//////////                        {
//////////                            if (hit.transform.name == "Onclick_isMove")
//////////                            {
//////////                                Onclick_Btn_isMove();
//////////                            }
//////////                            else if (hit.transform.name == "Onclick_isFight")
//////////                            {
//////////                                Onclick_Btn_isFight();
//////////                            }
//////////                        }
//////////                    }

//////////                    //현재 클릭된 그룹이 없을 때
//////////                    else
//////////                    {
//////////                        if (!hit.transform.name.Contains("Unit"))
//////////                        {
//////////                            Debug.Log(hit.transform.name);
//////////                        }
//////////                        //Team이 있는 오브젝트가 클릭 됐을 때 현재 클릭된 오브젝트에 클릭된 그룹의 부모 이름 입력
//////////                        else
//////////                        {
//////////                            if (hit.transform.name.Contains("Unit"))
//////////                            {
//////////                                currnet_SelectGroup_Txt.text = hit.transform.name;
//////////                                current_SelectGroup = hit.transform.gameObject;

//////////                                Time.timeScale = 0.1f;
//////////                            }
//////////                        }
//////////                    }
//////////                }
//////////            }

//////////            Camera.main.transform.position = new Vector3(getTouchDragValue().x, getTouchDragValue().y, -10);
//////////        }

//////////#if UNITY_EDITOR
//////////        CheckScroller();
//////////#else
//////////        CheckTouch();
//////////#endif
//////////        for (int i = 0; i < teamParent.Count; i++) {
//////////            if (teamParent[i].GetComponent<UnitManager>().unit_HP <= 0)
//////////            {
//////////                teamParent.Remove(teamParent[i]);
//////////            }
//////////        }
//////////    }

//////////    private Vector2 getTouchDragValue()
//////////    {
//////////        movePosDiff = Vector3.zero;

//////////        if (Input.touchCount == 1)
//////////        {
//////////            Touch touch = Input.GetTouch(0);
//////////            if (touch.phase == TouchPhase.Began)
//////////            {
//////////                prePos = touch.position - touch.deltaPosition;
//////////            }
//////////            else if (touch.phase == TouchPhase.Moved)
//////////            {
//////////                nowPos = touch.position - touch.deltaPosition;
//////////                movePosDiff = (Vector2)(prePos - nowPos) * Time.deltaTime;
//////////                prePos = touch.position - touch.deltaPosition;
//////////            }
//////////        }
//////////        return movePosDiff;
//////////    }

//////////    void CheckScroller() {
//////////        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

//////////        // scroll < 0 : scroll down하면 줌인 
//////////        if (Camera.main.orthographicSize <= 2.67f && scroll > 0)
//////////        {
//////////            temp_value = Camera.main.orthographicSize;
//////////            Camera.main.orthographicSize = temp_value; // maximize zoom in
//////////                                                  // 최대로 Zoom in 했을 때 특정 값을 지정했을 때
//////////                                                  // 최대 줌 인 범위를 벗어날 때 값에 맞추려고 한번 줌 아웃 되는 현상을 방지
//////////        }
//////////        // scroll > 0 : scroll up하면 줌아웃
//////////        else if (Camera.main.orthographicSize >= 7.03f && scroll < 0)
//////////        {
//////////            temp_value = Camera.main.orthographicSize;
//////////            Camera.main.orthographicSize = temp_value; // maximize zoom out 
//////////        }
//////////        else
//////////            Camera.main.orthographicSize -= scroll * 0.5f;
//////////    }

//////////    void CheckTouch()
//////////    {
//////////        int nTouch = Input.touchCount;
//////////        float m_fToucDis = 0f;
//////////        float fDis = 0f;

//////////        // 터치가 두개이고, 두 터치중 하나라도 이동한다면 카메라의 fieldOfView를 조정합니다.
//////////        if (Input.touchCount == 2 && (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved))
//////////        {
//////////            m_fToucDis = (Input.touches[0].position - Input.touches[1].position).sqrMagnitude;

//////////            fDis = (m_fToucDis - m_fOldToucDis) * 0.01f;

//////////            // 이전 두 터치의 거리와 지금 두 터치의 거리의 차이를 FleldOfView를 차감합니다.
//////////            m_fFieldOfView -= fDis;

//////////            // 최대는 100, 최소는 20으로 더이상 증가 혹은 감소가 되지 않도록 합니다.
//////////            m_fFieldOfView = Mathf.Clamp(m_fFieldOfView, 20.0f, 100.0f);

//////////            // 확대 / 축소가 갑자기 되지않도록 보간합니다.
//////////            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.fieldOfView, m_fFieldOfView, Time.deltaTime * 5);

//////////            m_fOldToucDis = m_fToucDis;
//////////        }
//////////    }

//////////    public void Onclick_Btn_isMove()
//////////    {
//////////        for (int i = 0; i < teamParent.Count; i++)
//////////        {
//////////            if (teamParent[i].transform.gameObject == current_SelectGroup)
//////////            {
//////////                //클릭 후 이동
//////////                teamParent[i].GetComponent<UnitManager>().moveDestination = new Vector3(current_DestinatoinPose.x, current_DestinatoinPose.y, 0);
//////////                teamParent[i].GetComponent<UnitManager>()._unitState = UnitManager.UnitState.RUN;
//////////                teamParent[i].GetComponent<UnitManager>().target.Clear();
//////////                currnet_SelectGroup_Txt.text = "";
//////////                current_SelectGroup = null;
//////////                selectButton.SetActive(false);
//////////            }
//////////        }

//////////        Time.timeScale = 1f;
//////////    }

//////////    public void Onclick_Btn_isFight()
//////////    {
//////////        for (int i = 0; i < teamParent.Count; i++)
//////////        {
//////////            if (teamParent[i].transform.gameObject == current_SelectGroup)
//////////            {
//////////                //클릭 후 이동
//////////                teamParent[i].GetComponent<UnitManager>().moveDestination = new Vector3(current_DestinatoinPose.x, current_DestinatoinPose.y, 0);
//////////                teamParent[i].GetComponent<UnitManager>()._unitState = UnitManager.UnitState.ATTACK;

//////////                currnet_SelectGroup_Txt.text = "";
//////////                current_SelectGroup = null;
//////////                selectButton.SetActive(false);
//////////            }
//////////        }

//////////        Time.timeScale = 1f;
//////////    }

//////////    public void Onclick_Btn_CreateUnit() {
//////////        int i = UnityEngine.Random.Range(0, unit.Length);

//////////        GameObject ins = Instantiate(unit[i], Vector3.zero, new Quaternion(0,0,0,0));
//////////        ins.transform.parent = unitParent.transform;

//////////        //for (int j = 1; j < unitParent.transform.childCount; j++)
//////////        //{
//////////        //    unitParent.transform.GetChild(j).GetComponent<UnitManager>().unitListUpdate();
//////////        //}
//////////    }

////////using System;
////////using System.Collections;
////////using System.Collections.Generic;
////////using System.Linq;
////////using UnityEngine;

////////public class UnitManager : MonoBehaviour
////////{

////////    public enum UnitId
////////    {
////////        PLAYER,
////////        UNIT,
////////        ENEMY
////////    }

////////    public enum UnitType
////////    {
////////        CLOSE,
////////        FAR
////////    }

////////    public enum UnitState
////////    {
////////        IDLE,
////////        RUN,
////////        ATTACK,
////////        SKILL,
////////        DEATH
////////    }

////////    public int unit_HP; //체력
////////    public int unit_ATK; //공격력
////////    public int unit_DEF; //방어력
////////    public int unit_RNG; //사거리
////////    public float unit_SPEED; //이동속unit_SPEED

////////    [HideInInspector] public int unit_FullHP; // 최초 체력
////////    [HideInInspector] public int unit_CNT; // 최초 유닛 숫자

////////    public GameObject grave;
////////    public GameObject enemyGroup;
////////    public List<GameObject> enemyGroupList = new List<GameObject>();

////////    public UnitId _unitId;
////////    public UnitType _unitType;
////////    public UnitState _unitState;

////////    public List<GameObject> target = new List<GameObject>();
////////    public List<GameObject> unitList = new List<GameObject>();

////////    public bool isAttackDmg = false;
////////    public bool isAttackAni = false;
////////    public bool isFollowAttack = false;

////////    public Vector3 moveDestination;

////////    private void Start()
////////    {
////////        switch (_unitId)
////////        {
////////            case UnitId.PLAYER:
////////                return;
////////            case UnitId.ENEMY:
////////                isFollowAttack = true;
////////                target.Add(GameObject.FindWithTag("Player").transform.gameObject);
////////                target = target.Distinct().ToList();
////////                _unitState = UnitState.ATTACK;
////////                GameObject.FindWithTag("CanvasManager").transform.gameObject.GetComponent<CanvasManager>().teamParent.Add(gameObject);
////////                enemyGroup = GameObject.FindWithTag("UnitGroup").transform.gameObject;
////////                grave = GameObject.FindWithTag("Grave").transform.gameObject;
////////                break;
////////            case UnitId.UNIT:
////////                grave = GameObject.FindWithTag("Grave").transform.gameObject;
////////                enemyGroup = GameObject.FindWithTag("EnemyGroup").transform.gameObject;
////////                GameObject.FindWithTag("CanvasManager").transform.gameObject.GetComponent<CanvasManager>().teamParent.Add(gameObject);
////////                _unitState = UnitState.RUN;
////////                float x = UnityEngine.Random.Range(-2f, 2f);
////////                float y = UnityEngine.Random.Range(-2f, 2f);
////////                moveDestination = new Vector3(x, y, 0);
////////                break;
////////        }

////////        unitListUpdate();

////////        unit_CNT = unitList.Count;
////////        unit_FullHP = unit_HP;
////////    }

////////    void Update()
////////    {
////////        try
////////        {
////////            unitListUpdate();
////////        }
////////        catch (Exception e)
////////        {

////////        }

////////        if (target.Count > 0)
////////        {
////////            for (int i = 0; i < enemyGroupList.Count; i++)
////////            {
////////                if (enemyGroupList[i].GetComponent<UnitManager>().target.Contains(this.gameObject))
////////                {
////////                    target.Add(enemyGroupList[i]);
////////                    target = target.Distinct().ToList();
////////                    //Debug.Log(target.Count);
////////                    //swap(target, 0, target.Count-1);
////////                }
////////            }
////////        }

////////        if (enemyGroupList.Count > 0)
////////        {
////////            for (int i = 0; i < enemyGroupList.Count; i++)
////////            {
////////                if (enemyGroupList[i].GetComponent<UnitManager>().unit_HP <= 0)
////////                {
////////                    enemyGroupList.RemoveAt(i);
////////                }
////////            }
////////        }

////////        if (_unitId == UnitId.PLAYER)
////////        {
////////            return;
////////        }

////////        else if (_unitId == UnitId.ENEMY)
////////        {
////////            if (target.Count == 0)
////////            {
////////                isFollowAttack = true;
////////                target.Add(GameObject.FindWithTag("Player").transform.gameObject);
////////                target = target.Distinct().ToList();
////////                _unitState = UnitState.ATTACK;
////////            }
////////        }

////////        switch (_unitState)
////////        {
////////            case UnitState.IDLE:
////////                IDLE();
////////                break;
////////            case UnitState.RUN:
////////                Run();
////////                break;
////////            case UnitState.ATTACK:
////////                Attack();
////////                break;
////////            case UnitState.SKILL:
////////                Skill();
////////                break;
////////            case UnitState.DEATH:
////////                Death();
////////                break;
////////        }

////////        if (unit_HP <= 0)
////////        {
////////            _unitState = UnitState.DEATH;

////////            for (int i = 0; i < enemyGroupList.Count; i++)
////////            {
////////                enemyGroupList[i].GetComponent<UnitManager>().target.Remove(gameObject);
////////            }

////////            Destroy(this.gameObject);
////////        }
////////    }

////////    public void unitListUpdate()
////////    {
////////        for (int j = 0; j < enemyGroup.transform.childCount; j++)
////////        {
////////            enemyGroupList.Add(enemyGroup.transform.GetChild(j).gameObject);
////////            enemyGroupList = enemyGroupList.Distinct().ToList();
////////        }

////////        for (int i = 0; i < transform.childCount; i++)
////////        {
////////            unitList.Add(transform.GetChild(i).gameObject);
////////            unitList = unitList.Distinct().ToList();
////////        }
////////    }

////////    private void RunToDestinatoin(Vector3 destinatoin)
////////    {
////////        for (int i = 0; i < unitList.Count; i++)
////////        {
////////            unitList[i].transform.GetComponentInChildren<Animator>().SetBool("Attack", false);
////////            unitList[i].transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.3f);
////////        }

////////        transform.position = Vector3.MoveTowards(transform.position, destinatoin, unit_SPEED * Time.timeScale);
////////    }

////////    private void LookAtDestination(float destinationX)
////////    {
////////        for (int i = 0; i < unitList.Count; i++)
////////        {
////////            if (unitList[i].transform.position.x < destinationX)
////////            {
////////                unitList[i].transform.localEulerAngles = new Vector3(0, 180, 0);
////////            }
////////            else
////////            {
////////                unitList[i].transform.localEulerAngles = Vector3.zero;
////////            }
////////        }
////////    }

////////    private IEnumerator AttackDmg()
////////    {
////////        isAttackDmg = true;

////////        if (target[0].GetComponent<UnitManager>().unit_HP > 0)
////////        {
////////            yield return new WaitForSeconds(1f);
////////            --target[0].GetComponent<UnitManager>().unit_HP;

////////            for (int i = 0; i < unit_ATK; i++)
////////            {
////////                if (Mathf.Round(unit_CNT * (float)target[0].GetComponent<UnitManager>().unit_HP / (float)target[0].GetComponent<UnitManager>().unit_FullHP)
////////                    != target[0].GetComponent<UnitManager>().unitList.Count)
////////                {
////////                    if (target[0].GetComponent<UnitManager>().unitList.Count > 0)
////////                    {
////////                        int rand = UnityEngine.Random.Range(0, target[0].GetComponent<UnitManager>().unitList.Count);
////////                        target[0].GetComponent<UnitManager>().unitList[rand].GetComponentInChildren<Animator>().SetTrigger("Die");
////////                        target[0].GetComponent<UnitManager>().unitList[rand].transform.SetParent(grave.transform);
////////                        target[0].GetComponent<UnitManager>().unitList.RemoveAt(rand);
////////                    }
////////                }

////////                /*if (target[0].GetComponent<UnitManager>().unit_HP == 1)
////////                {
////////                    target[0].GetComponent<UnitManager>().unitList[0].GetComponentInChildren<Animator>().SetTrigger("Die");
////////                    target[0].GetComponent<UnitManager>().unitList[0].transform.SetParent(grave.transform);
////////                    target[0].GetComponent<UnitManager>().unitList.RemoveAt(0);
////////                    target[0].GetComponent<BoxCollider2D>().enabled = false;
////////                }*/
////////            }
////////        }
////////        else if (target[0].GetComponent<UnitManager>().unit_HP <= 0)
////////        {
////////            if (!isFollowAttack)
////////            {
////////                target.Remove(target[0]);
////////                isAttackDmg = false;
////////                isAttackAni = false;

////////                StopAllCoroutines();
////////            }
////////            else
////////            {
////////                target.Remove(target[0]);
////////                isAttackDmg = false;
////////                isAttackAni = false;
////////                isFollowAttack = false;

////////                _unitState = UnitState.IDLE;
////////                StopAllCoroutines();
////////            }
////////        }


////////        isAttackDmg = false;
////////    }

////////    private IEnumerator AttackAni()
////////    {
////////        isAttackAni = true;
////////        for (int i = 0; i < transform.childCount; i++)
////////        {
////////            transform.GetChild(i).GetComponentInChildren<Animator>().SetBool("Attack", true);
////////            yield return new WaitForSeconds(0.1f);
////////        }
////////    }

////////    private void swap(List<GameObject> list, int from, int to)
////////    {
////////        GameObject tmp = list[from];
////////        list[from] = list[to];
////////        list[to] = tmp;
////////    }
////////    /// <summary>
////////    /// State Method List
////////    /// 해당 상태가 되었을 때 UPDATE로 돌아야 할 함
////////    /// </summary>

////////    private void IDLE()
////////    {
////////        //적을 타겟으로 추가
////////        if (enemyGroupList.Count > 0)
////////        {
////////            for (int i = 0; i < enemyGroupList.Count; i++)
////////            {
////////                if (Vector2.Distance(transform.position, enemyGroupList[i].transform.position) < unit_RNG)
////////                {
////////                    if (enemyGroupList[i].GetComponent<UnitManager>().unit_HP > 0)
////////                    {
////////                        target.Add(enemyGroupList[i]);
////////                        target = target.Distinct().ToList();
////////                    }

////////                    isFollowAttack = true;
////////                    _unitState = UnitState.ATTACK;
////////                }
////////                else
////////                {
////////                    StopAllCoroutines();
////////                    //target.Clear();
////////                    isAttackDmg = false;
////////                    isAttackAni = false;

////////                    for (int k = 0; k < transform.childCount; k++)
////////                    {
////////                        transform.GetChild(k).transform.GetComponentInChildren<Animator>().SetBool("Attack", false);
////////                        transform.GetChild(k).transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
////////                    }
////////                }
////////            }
////////        }

////////        else
////////        {
////////            StopAllCoroutines();
////////            //target.Clear();
////////            isAttackDmg = false;
////////            isAttackAni = false;

////////            for (int k = 0; k < transform.childCount; k++)
////////            {
////////                transform.GetChild(k).transform.GetComponentInChildren<Animator>().SetBool("Attack", false);
////////                transform.GetChild(k).transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
////////            }
////////        }

////////        if (target.Count > 0)
////////        {
////////            _unitState = UnitState.ATTACK;
////////        }
////////    }

////////    private void Run()
////////    {
////////        StopAllCoroutines();
////////        //target.Clear();
////////        isAttackDmg = false;
////////        isAttackAni = false;

////////        if (Vector2.Distance(moveDestination, transform.position) > 0.1f)
////////        {
////////            RunToDestinatoin(moveDestination);
////////            LookAtDestination(moveDestination.x);
////////        }
////////        //움직임으로 도착하면 리셋
////////        else
////////        {
////////            _unitState = UnitState.IDLE;
////////        }
////////    }

////////    private void Attack()
////////    {
////////        //적을 타겟으로 추가
////////        for (int i = 0; i < enemyGroupList.Count; i++)
////////        {
////////            if (Vector2.Distance(transform.position, enemyGroupList[i].transform.position) < unit_RNG)
////////            {
////////                if (enemyGroupList[i].GetComponent<UnitManager>().unit_HP > 0)
////////                {
////////                    target.Add(enemyGroupList[i]);
////////                    target = target.Distinct().ToList();
////////                }
////////            }
////////        }

////////        if (target.Count > 1)
////////        {
////////            for (int i = 1; i < target.Count; i++)
////////            {
////////                if (Vector2.Distance(transform.position, target[0].transform.position) > Vector2.Distance(transform.position, target[i].transform.position))
////////                {
////////                    swap(target, 0, i);
////////                }
////////            }
////////        }

////////        //클릭으로 이동 했을 때
////////        if (!isFollowAttack)
////////        {
////////            //필드에 적이 있을때
////////            if (enemyGroupList.Count > 0)
////////            {
////////                for (int i = 0; i < enemyGroupList.Count; i++)
////////                {
////////                    //타겟이 없을때
////////                    if (target.Count == 0)
////////                    {
////////                        isAttackDmg = false;
////////                        isAttackAni = false;

////////                        //범위내에 적이 없을때
////////                        if (Vector2.Distance(transform.position, enemyGroupList[i].transform.position) > unit_RNG)
////////                        {
////////                            //클릭 방향으로 이동
////////                            if (Vector2.Distance(moveDestination, transform.position) > 0.1f)
////////                            {
////////                                RunToDestinatoin(moveDestination);
////////                                LookAtDestination(moveDestination.x);
////////                            }
////////                            //도착시 대기
////////                            else
////////                            {
////////                                _unitState = UnitState.IDLE;
////////                            }
////////                        }
////////                    }

////////                    //타겟이 있을때
////////                    else
////////                    {
////////                        //사거리내 일 때
////////                        if (Vector2.Distance(transform.position, target[0].transform.position) < unit_RNG)
////////                        {
////////                            //공격
////////                            if (!isAttackDmg)
////////                            {
////////                                StartCoroutine(AttackDmg());
////////                            }
////////                            if (!isAttackAni)
////////                            {
////////                                StartCoroutine(AttackAni());
////////                            }

////////                            try
////////                            {
////////                                //적들중 체력이 0인 적 삭제
////////                                if (enemyGroupList[i].GetComponent<UnitManager>().unit_HP <= 0)
////////                                {
////////                                    enemyGroupList.RemoveAt(i);
////////                                }
////////                            }
////////                            catch (Exception e)
////////                            {
////////                                Debug.Log("해당 유닛이 아닌 다른 유닛이 group list에서 지웠을 때");
////////                            }
////////                        }
////////                        //사거리 밖일 때
////////                        else
////////                        {
////////                            //적이 멀어지면 따라감
////////                            isAttackDmg = false;
////////                            isAttackAni = false;
////////                            StopAllCoroutines();

////////                            RunToDestinatoin(target[0].transform.position);
////////                            LookAtDestination(target[0].transform.position.x);
////////                        }

////////                        //적 그룹 리스트중 타겟이 된 적
////////                        try
////////                        {
////////                            if (enemyGroupList[i] == target[0])
////////                            {
////////                                if (!isAttackDmg)
////////                                {
////////                                    StartCoroutine(AttackDmg());
////////                                }
////////                                if (!isAttackAni)
////////                                {
////////                                    StartCoroutine(AttackAni());
////////                                }
////////                            }
////////                        }
////////                        catch (Exception e)
////////                        {
////////                            Debug.Log("해당 유닛이 아닌 다른 유닛이 group list에서 지웠을 때");
////////                        }
////////                    }
////////                }
////////            }
////////            //필드에 적이 없을때
////////            else
////////            {
////////                //클릭한 방향으로 이동
////////                if (Vector2.Distance(moveDestination, transform.position) > 0.1f)
////////                {
////////                    RunToDestinatoin(moveDestination);
////////                    LookAtDestination(moveDestination.x);
////////                }
////////                //도착시 대기
////////                else
////////                {
////////                    isAttackDmg = false;
////////                    isAttackAni = false;

////////                    _unitState = UnitState.IDLE;
////////                }
////////            }
////////        }

////////        //IDLE에서 ATTACK으로 넘어왔을 때
////////        else
////////        {
////////            //필드에 적이 있을때
////////            if (enemyGroupList.Count > 0)
////////            {
////////                for (int i = 0; i < enemyGroupList.Count; i++)
////////                {
////////                    //타겟이 없을때
////////                    if (target.Count == 0)
////////                    {
////////                        isAttackDmg = false;
////////                        isAttackAni = false;
////////                    }
////////                    //타겟이 있을때
////////                    else
////////                    {
////////                        //사거리내 일 때
////////                        if (Vector2.Distance(transform.position, target[0].transform.position) < unit_RNG)
////////                        {
////////                            //공격
////////                            if (!isAttackDmg)
////////                            {
////////                                StartCoroutine(AttackDmg());
////////                            }
////////                            if (!isAttackAni)
////////                            {
////////                                StartCoroutine(AttackAni());
////////                            }
////////                            //적들중 체력이 0인 적 삭제
////////                            if (enemyGroupList[i].GetComponent<UnitManager>().unit_HP <= 0)
////////                            {
////////                                enemyGroupList.RemoveAt(i);
////////                            }
////////                        }
////////                        //사거리 밖일 때
////////                        else
////////                        {
////////                            //적이 멀어지면 따라감
////////                            isAttackDmg = false;
////////                            isAttackAni = false;
////////                            StopAllCoroutines();

////////                            RunToDestinatoin(target[0].transform.position);
////////                            LookAtDestination(target[0].transform.position.x);
////////                        }
////////                    }
////////                }
////////            }
////////            //필드에 적이 없을때
////////            else
////////            {
////////                isAttackDmg = false;
////////                isAttackAni = false;
////////                isFollowAttack = false;

////////                _unitState = UnitState.IDLE;
////////            }
////////        }
////////    }

////////    private void Skill()
////////    {
////////        StartCoroutine(transform.GetComponent<SkillManager>().Skill());
////////    }

////////    private void Death()
////////    {
////////        for (int i = 0; i < transform.childCount; i++)
////////        {
////////            transform.GetChild(i).transform.GetComponentInChildren<Animator>().SetBool("Attack", false);
////////            transform.GetChild(i).transform.GetComponentInChildren<Animator>().SetFloat("RunState", 0.0f);
////////        }
////////        target.Clear();
////////        StopAllCoroutines();
////////        transform.GetComponent<UnitManager>().enabled = false;
////////        transform.GetComponent<BoxCollider2D>().enabled = false;
////////    }
////////}
//////using System.Collections;
//////using System.Collections.Generic;
//////using System.Linq;
//////using UnityEngine;

//////public class UnityColliderManager : MonoBehaviour
//////{
//////    private UnitManager unitManager;

//////    private void Start()
//////    {
//////        unitManager = transform.parent.GetComponent<UnitManager>();
//////    }

//////    private void Update()
//////    {
//////        if (transform.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("3_Death"))
//////        {
//////            StartCoroutine(Die(transform.gameObject));
//////        }
//////    }

//////    IEnumerator Die(GameObject obj)
//////    {
//////        yield return new WaitForSeconds(1f);
//////        Destroy(obj);
//////    }
//////}
////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class SkillManager : MonoBehaviour
////{
////    private float skillDelay;
////    public float skillDelayTime;
////    public float skillCastingTime;

////    private bool isUseSkill;
////    [SerializeField] private GameObject skillEffect;
////    [SerializeField] private GameObject skillEffect2;
////    [SerializeField] private int skill_ATK;
////    private bool isSkillATK = true;

////    private void Start()
////    {
////        skillDelay = 0;
////    }

////    private void Update()
////    {
////        if (transform.GetComponent<UnitManager>()._unitState != UnitManager.UnitState.SKILL && transform.GetComponent<UnitManager>()._unitState == UnitManager.UnitState.ATTACK)
////        {
////            if (!isUseSkill)
////            {
////                skillDelay += Time.deltaTime;
////            }
////        }

////        if (transform.GetComponent<UnitManager>()._unitState == UnitManager.UnitState.ATTACK && transform.GetComponent<UnitManager>().target.Count > 0)
////        {
////            if (skillDelay > skillDelayTime)
////            {
////                if (!isUseSkill)
////                {
////                    StartCoroutine(Skillcastreset());
////                    transform.GetComponent<UnitManager>()._unitState = UnitManager.UnitState.SKILL;
////                    skillDelay = 0;
////                }
////            }
////        }
////    }

////    IEnumerator Skillcastreset()
////    {
////        isUseSkill = true;
////        yield return new WaitForSeconds(skillCastingTime);
////        skillEffect.SetActive(false);
////        skillEffect2.SetActive(false);
////        transform.GetComponent<UnitManager>()._unitState = UnitManager.UnitState.ATTACK
////            ;
////        isUseSkill = false;
////        isSkillATK = true;
////    }

////    public IEnumerator Skill()
////    {
////        skillEffect.SetActive(true);
////        skillEffect.transform.position = transform.position;
////        skillEffect2.SetActive(true);
////        skillEffect2.transform.position = transform.GetComponent<UnitManager>().target[0].transform.position + (Vector3.up * 0.5f);

////        if (isSkillATK)
////        {
////            if (transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unit_HP > 0)
////            {
////                for (int i = 0; i < skill_ATK; i++)
////                {
////                    transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unit_HP--;

////                    if (Mathf.Round(transform.GetComponent<UnitManager>().unit_CNT * (float)transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unit_HP /
////                        (float)transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unit_FullHP)
////                        != transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList.Count)
////                    {
////                        if (transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList.Count > 0)
////                        {
////                            int rand = UnityEngine.Random.Range(0, transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList.Count);
////                            transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList[rand].GetComponentInChildren<Animator>().SetTrigger("Die");
////                            transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList[rand].transform.SetParent(transform.GetComponent<UnitManager>().grave.transform);
////                            transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unitList.RemoveAt(rand);
////                        }
////                    }
////                }
////                isSkillATK = false;
////            }
////            else if (transform.GetComponent<UnitManager>().target[0].GetComponent<UnitManager>().unit_HP <= 0)
////            {
////                if (!transform.GetComponent<UnitManager>().isFollowAttack)
////                {
////                    transform.GetComponent<UnitManager>().target.Remove(transform.GetComponent<UnitManager>().target[0]);
////                    transform.GetComponent<UnitManager>().isAttackDmg = false;
////                    transform.GetComponent<UnitManager>().isAttackAni = false;

////                    StopAllCoroutines();
////                }
////                else
////                {
////                    transform.GetComponent<UnitManager>().target.Remove(transform.GetComponent<UnitManager>().target[0]);
////                    transform.GetComponent<UnitManager>().isAttackDmg = false;
////                    transform.GetComponent<UnitManager>().isAttackAni = false;
////                    transform.GetComponent<UnitManager>().isFollowAttack = false;

////                    transform.GetComponent<UnitManager>()._unitState = UnitManager.UnitState.IDLE;
////                    StopAllCoroutines();
////                }
////            }
////            yield return new WaitForSeconds(0.1f);
////        }

////        //skill animation
////        //skill collider create
////    }
////}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyManager : MonoBehaviour
//{
//    [SerializeField] private GameObject[] enemyList;
//    [SerializeField] private GameObject[] randomPose;
//    [SerializeField] private GameObject enemyParent;

//    private void Start()
//    {
//        InvokeRepeating("CreateEnemy", 10f, 10f);
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            randomPose[i] = transform.GetChild(i).gameObject;
//        }
//    }

//    private void CreateEnemy()
//    {
//        GameObject ins;
//        int i = UnityEngine.Random.Range(0, enemyList.Length);
//        int j = UnityEngine.Random.Range(0, randomPose.Length);
//        ins = Instantiate(enemyList[i], randomPose[j].transform.position, new Quaternion(0, 0, 0, 0));
//        ins.transform.parent = enemyParent.transform;
//        //for (int k = 0; k < enemyParent.transform.childCount; k++)
//        //{
//        //    enemyParent.transform.GetChild(k).GetComponent<UnitManager>().unitListUpdate();
//        //}
//    }
//}
