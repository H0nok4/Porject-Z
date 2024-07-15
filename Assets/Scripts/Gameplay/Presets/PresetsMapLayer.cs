using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PresetsMapLayer : MonoBehaviour
{
    [SerializeField] private int _mapLayer;

    public int MapLayer => _mapLayer;

}