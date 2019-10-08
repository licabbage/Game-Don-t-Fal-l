

//////////////////////////////////////////////////////
// MK Glow Cube                      				//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de | www.michaelkremmel.store //
// Copyright © 2017 All rights reserved.            //
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Glow.Example
{
    public class m_Emission : MonoBehaviour
    {
        private int _emissionColorId;
        private Material _usedMaterial;
        [SerializeField]
        private MinMaxRange _emissionColorIntensity = new MinMaxRange(1.0f, 1.5f);
        private float _currentColorIntensity = 1;
        private Color _currentColor = new Color();

        private Material _baseMaterial;

        MeshRenderer dsd;
        private void Awake()
        {
           
            _emissionColorId = Shader.PropertyToID("_EmissionColor");
            _baseMaterial = new Material(GetComponent<Renderer>().material);
            _usedMaterial = new Material(_baseMaterial);
            _currentColorIntensity = Random.Range(_emissionColorIntensity.minValue, _emissionColorIntensity.maxValue);
            _usedMaterial.SetColor(_emissionColorId, _currentColor * _currentColorIntensity);
        }

        private void Update()
        {
            _currentColorIntensity = Random.Range(_emissionColorIntensity.minValue, _emissionColorIntensity.maxValue);
            _usedMaterial.SetColor(_emissionColorId, _currentColor * _currentColorIntensity);
        }
    }
}
