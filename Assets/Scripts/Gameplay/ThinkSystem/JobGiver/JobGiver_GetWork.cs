using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JobGiver_GetWork : ThinkNode {
    public override ThinkResult GetResult(Thing_Unit unit) {
        var workGiverList = unit.WorkSetting.UsedWorkGivers;
        foreach (var workGiver in workGiverList) {
            Job nonScanJob = workGiver.NonScanJob(unit);
            if (nonScanJob != null) {
                return new ThinkResult(nonScanJob, this);
            }

            WorkGiver_Scanner scanner = workGiver as WorkGiver_Scanner;
            if (scanner != null) {
                if (scanner.Def.ScanThings) {
                    //TODO：扫描Thing
                    Predicate<Thing> validator = (Thing t) => scanner.HasJobOnThing(unit, t);
                    IEnumerable<Thing> scannedThings = scanner.CanWorkThings(unit);
                    Thing findResult = null;
                    if (scannedThings == null) {
                        scannedThings = MapController.Instance.Map.ListThings.ThingsMatching(scanner.ThingRequest);
                    }

                    foreach (var scannedThing in scannedThings) {
                        if (validator(scannedThing)) {
                            findResult = scannedThing;
                            break;
                        }
                    }

                    if (findResult != null) {
                        return new ThinkResult(scanner.JobOnThing(unit, findResult), this);
                    }
                }

                if (scanner.Def.ScanSections) {
                    //TODO: 扫描地图格子
                }
            }
        }

        Debug.LogError("没有扫描到可以用的工作");

        return ThinkResult.NoJob;
    }
}