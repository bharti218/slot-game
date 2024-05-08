using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace slotgame
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;

        [SerializeField] TextMeshProUGUI totalBetTxt;
        [SerializeField] TextMeshProUGUI balanceText;
        [SerializeField] TextMeshProUGUI betTxt;

        private float betAmount;
        private float balance;
        private float totalBet;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void SetBetAmount(float _betAmount)
        {
            betAmount = _betAmount;
            betTxt.text = "BET: " + betAmount.ToString();
        }

        public void SetBalance(float _balance)
        {
            balance = _balance;
            balanceText.text = "WIN: " + _balance.ToString();
        }

        public void SetTotalBet(float _totalBet)
        {
            totalBet = _totalBet;
            totalBetTxt.text = "TOTAL BET: " + _totalBet.ToString();
        }

        public void OnSpin()
        {
            balance -= betAmount;
            totalBet += betAmount;
            SetTotalBet(totalBet);
            SetBalance(balance);

        }

        public void OnWinning(float winningAmount)
        {
            balance += winningAmount;
            SetBalance(balance);
        }

    }

}