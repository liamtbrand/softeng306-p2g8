using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Maintains a list of scheduled actions to perform.
// Not concurrent
public class NPCActionQueue {

    //public enum Action { IDLE, WALK, CODE, DANCE, FACE };

    //public class ActionArgs {

    //}

    //public class WalkActionArgs : ActionArgs {
    //    public List<Vector2> waypoints;
    //}

    //public class DanceActionArgs {
    //    public long timeout;
    //}

    //public class IdleActionArgs {
    //    public long timeout;
    //}

    //public class CodeActionArgs {
    //    public long timeout;
    //}

    //public class FaceActionArgs {
    //    public Vector2 direction;
    //}

    //// main action queue.
    //private Queue<Action> actionQueue;

    //// queue for each action.
    //private Queue<WalkActionArgs> walkActionArgs;
    //private Queue<DanceActionArgs> danceActionArgs;
    //private Queue<IdleActionArgs> idleActionArgs;
    //private Queue<CodeActionArgs> codeActionArgs;
    //private Queue<FaceActionArgs> faceActionArgs;

    //public NPCActionQueue() {
    //    actionQueue = new Queue<Action>();
    //    walkActionArgs = new Queue<WalkActionArgs>();
    //    danceActionArgs = new Queue<DanceActionArgs>();
    //    idleActionArgs = new Queue<IdleActionArgs>();
    //    codeActionArgs = new Queue<CodeActionArgs>();
    //    faceActionArgs = new Queue<FaceActionArgs>();
    //}

    //public void EnqueueWalk(WalkActionArgs walkActionArgs) {
    //    actionQueue.Enqueue(Action.WALK);
    //    walkActionArgs.Enqueue(walkActionArgs);
    //}

    //public void EnqueueDance(DanceActionArgs danceActionArgs) {
    //    actionQueue.Enqueue(Action.DANCE);
    //    danceActionArgs.Enqueue(danceActionArgs);
    //}

    //public void EnqueueIdle(IdleActionArgs idleActionArgs) {
    //    actionQueue.Enqueue(Action.IDLE);
    //    idleActionArgs.Enqueue(idleActionArgs);
    //}

    //public void EnqueueCode(CodeActionArgs codeActionArgs) {
    //    actionQueue.Enqueue(Action.CODE);
    //    codeActionArgs.Enqueue(codeActionArgs);
    //}

    //public void EnqueueFace(FaceActionArgs faceActionArgs) {
    //    actionQueue.Enqueue(Action.FACE);
    //    faceActionArgs.Enqueue(faceActionArgs);
    //}

    //public bool HasNextAction() {
    //    return actionQueue.Count() > 0;
    //}

    //public Action PeekNextAction() {
    //    return actionQueue.Peek();
    //}

    //public WalkActionArgs PopWalkActionArgs() {
    //    actionQueue.Pop();
    //    return walkActionArgs.Pop();
    //}

    //public DanceActionArgs PopDanceActionArgs() {
    //    actionQueue.Pop();
    //    return danceActionArgs.Pop();
    //}

    //public IdleActionArgs PopIdleActionArgs() {
    //    actionQueue.Pop();
    //    return idleActionArgs.Pop();
    //}

    //public CodeActionArgs PopCodeActionArgs() {
    //    actionQueue.Pop();
    //    return codeActionArgs.Pop();
    //}

    //public FaceActionArgs PopFaceActionArgs() {
    //    actionQueue.Pop();
    //    return faceActionArgs.Pop();
    //}

    //// Clear the actionQueue
    //public void Clear()
    //{
    //    actionQueue.Clear();
    //    walkActionArgs.Clear();
    //    danceActionArgs.Clear();
    //    idleActionArgs.Clear();
    //    codeActionArgs.Clear();
    //    faceActionArgs.Clear();
    //}

}
