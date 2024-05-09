# Level Generator Config: 
1. AnimDuration: animation duration for symbols for play falling animation.
2. Min Match Symbol: minimum matching symbols required to win the round.
3. Max Row: number of rows in the grid
4. Max Col: number of columns in the grid
5. Bet Amount
6. Balance:
7. Winning Factor: Using this formula to calculate the winning amount: Matched Symbol Count*Winning Factor


# Slot Game
This is the slot game implementation using a state machine pattern. There are four states in this game namely: 
1. Idle State
2. Spin State
3. Result State
4. Respawn State

# State Machine
The game transitions among these states are given in the following diagram.
![State Machine Diagram](/Screenshots/IMG20240504005753.jpg)


