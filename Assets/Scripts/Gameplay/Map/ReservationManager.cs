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

        private int _maxUnitCount;
        public int MaxUnitCount => _maxUnitCount;

        public Reservation(Thing_Unit unit, Job job, JobTargetInfo targetInfo, int maxUnitCount) {
            Unit = unit;
            Job = job;
            TargetInfo = targetInfo;
            _maxUnitCount = maxUnitCount;
        }
    }

    public List<Reservation> Reservations = new List<Reservation>();

    private Map Map => MapController.Instance.Map;

    public bool Reserve(Thing_Unit unit,Job job,JobTargetInfo targetInfo,int maxUnitCount = 1) {
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

        if (!CanReserve(unit,targetInfo,maxUnitCount)) {

            return false;
        }

        Reservations.Add(new Reservation(unit,job,targetInfo, maxUnitCount));
        return true;
    }

    public bool CanReserve(Thing_Unit unit, JobTargetInfo targetInfo,int maxUnitCount = 1,bool ignoreOtherReservation = false) {
        if (unit == null) {
            return false;
        }

        if (!targetInfo.IsValid) {
            return false;
        }
        //TODO:查看是否有冲突的预定

        if (!ignoreOtherReservation)
        {
            int sameReservationUnitCount = 0;
            for (int i = 0; i < Reservations.Count; i++) {
                Reservation reservation = Reservations[i];
                if (reservation.TargetInfo == targetInfo && reservation.Unit != unit) {

                    //相当于是俩类的Reservation,直接返回false
                    if (reservation.MaxUnitCount != maxUnitCount)
                    {
                        return false;
                    }

                    sameReservationUnitCount++;

                    if (sameReservationUnitCount >= reservation.MaxUnitCount)
                    {
                        //重复预定的人太多了,返回false
                        return false;
                    }

                    return false;
                }
            }
        }
        

        return true;
    }

    public void ClearReservationByJob(Thing_Unit unit, Job job) {
        //TODO:清理掉由这个单位创建的Reservation
    }
}