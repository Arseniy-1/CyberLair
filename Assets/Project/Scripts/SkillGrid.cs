using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillGrid : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private Image[] _bulletIcons;

    private  CompositeDisposable _disposable = new();

    private void Awake()
    {
        MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe((message) => ShowSkills())
            .AddTo(_disposable);
    }
    
    private void ShowSkills()
    {
        for(int i = 0; i < _mediator.RaisedSkills.Count; i++)
        {
            _bulletIcons[i].sprite = _mediator.RaisedSkills[i].SkillInfo.Icon;
            _bulletIcons[i].gameObject.SetActive(true);
        }
    }
}