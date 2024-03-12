using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReservationManager : Singleton<ReservationManager> {

    public class Reservation {
        public Thing_Unit Unit;

        public Job Job;

        public JobTargetInfo TargetInfo;


    }

    public List<Reservation> Reservations = new List<Reservation>();

    private Map Map => MapController.Instance.Map;

    public bool Reserve(Thing_Unit unit,Job job,JobTargetInfo targetInfo) {
        //TODO:先看是否有相同的预定
        if (unit == null) {
            return false;
        }

        for (int i = 0; i < Reservations.Count; i++) {
            var reservation = Reservations[i];
            if (reservation.Unit == unit && reservation.Job == job && reservation.TargetInfo == targetInfo) {
                Debug.Log("有相同的预定,直接返回True");
                return true;
            }
        }

        if (!CanReserve(unit,targetInfo)) {

            return false;
        }

        Reservations.Add(new Reservation(){Unit = unit,Job = job,TargetInfo = targetInfo});
        return true;
    }

    public bool CanReserve(Thing_Unit unit, JobTargetInfo targetInfo,bool ignoreOtherReservation = false) {
        if (unit == null) {
            return false;
        }

        if (!targetInfo.IsValid) {
            return false;
        }
        //TODO:查看是否有冲突的预定

        for (int i = 0; i < Reservations.Count; i++) {
            var reservation = Reservations[i];
            if (reservation.Unit == unit || reservation.TargetInfo == targetInfo) {
                return false;
            }
        }

        return true;
    }

    public void ClearReservationByJob(Thing_Unit unit, Job job) {
        //TODO:清理掉由这个单位创建的Reservation
    }
}