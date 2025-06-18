using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SkillGrid : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    [SerializeField] private Image[] _skillIcons;

    private  CompositeDisposable _disposable = new();

    // private void Start()
    // {
    //     MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe((message) => ShowSkills())
    //         .AddTo(_disposable);
    // }
    //

    private void OnDisable()
    {
        _disposable?.Clear();
    }

    public void ShowSkills()
    {
        for(int i = 0; i < _mediator.RaisedSkills.Count; i++)
        {
            _skillIcons[i].sprite = _mediator.RaisedSkills[i].SkillInfo.Icon;
            _skillIcons[i].gameObject.SetActive(true);
        }
    }
}