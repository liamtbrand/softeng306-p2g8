namespace NPCScripts.StaffStateScripts
{
    public class StaffMentalState
    {
        public enum State { NORMAL, ANNOYED, ABOUT_TO_LEAVE, READY_TO_LEAVE }
        public double GenderDiversityScore = 0; // negative = low diversity. See StaffStateDialogueManager for use
        public double AgeDiversityScore = 0;
        public State StaffState = State.NORMAL;
    }
}