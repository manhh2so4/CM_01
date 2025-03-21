
namespace HStrong.Quests
{
    [System.Serializable]
    public class QuestStepState {
        public string status;
        public QuestStepStatus state;

        public QuestStepState(string status, QuestStepStatus state)
        {
            this.status = status;
            this.state = state;
        }
        public QuestStepState()
        {
            this.status = "";
            this.state = QuestStepStatus.NOT_STARTED;
        }
    }

}
public enum QuestStepStatus {
    NOT_STARTED,
    IN_PROGRESS,
    COMPLETED
}