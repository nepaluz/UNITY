using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// �������� ���������� �����
	public Transform enemy;

	// ��������� ���������� ����� ���������, ���-�� ������
	public float timeBeforeSpawning = 1.5f;
	public float timeBetweenEnemies = 0.25f;
	public float timeBeforeWaves = 2.0f;
	public int enemiesPerWave = 10;
	private int currentNumberOfEnemies = 0;
    private int die=0;

    [SerializeField] private TextMesh scoreLabel;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemies());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ��������� ���� ������
	IEnumerator SpawnEnemies()
	{
		// ��������� �������� ����� ������ ���������� ������
		yield return new WaitForSeconds (timeBeforeSpawning);
		// ����� ������ ������, �������� ����������� ��� ��������
		while(true)
		{
			// �� ��������� ����� ������, ���� �� ���������� ������
			if (currentNumberOfEnemies <= 0)
			{
				float randDirection;
				float randDistance;
				// ������� 10 ������ � ��������� ������ �� �������
				for (int i = 0; i < enemiesPerWave; i++)
				{
					// ����� ��������� ���������� ��� ���������� � �����������
					randDistance = Random.Range (2, 6);
					randDirection = Random.Range (0, 360);
					// ���������� ���������� ��� ������� ��������� ��������� �����
					float posX = this. transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
					float posY = this. transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);
					// ������ ����� �� �������� �����������
					Instantiate (enemy, new Vector3 (posX, posY, 0), this.transform.rotation);
					currentNumberOfEnemies++;
					yield return new WaitForSeconds (timeBetweenEnemies);
				}
			}
			// �������� �� ��������� ��������
			yield return new WaitForSeconds (timeBeforeWaves);
		}
	}

	// ��������� ���������� ���������� ������ � ����������
	public void KilledEnemy()
	{
		currentNumberOfEnemies--;
        die++;
        StartCoroutine(CheckMatch());
    }

    private IEnumerator CheckMatch()
    {
        scoreLabel.text = "Score: " + die;
        yield return new WaitForSeconds(.5f);
    }
}
