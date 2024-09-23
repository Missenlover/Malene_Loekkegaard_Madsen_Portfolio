import gymnasium as gym
import numpy as np
import random


# Define the game
game = "Blackjack-v1"

# Initialize the environment
env = gym.make(game)

# Set the discount factor and learning rate
discount_factor = 0.9
learning_rate = 0.1

# Initialize the Q-table with all possible state-action pairs
Q = {}
for player_sum in range(12, 22):  # Possible player sums
    for dealer_card in range(1, 11):  # Possible dealer showing cards
        for has_usable_ace in [False, True]:  # Possible states for usable ace
            for action in range(env.action_space.n):  # Possible actions (hit or stand)
                state = (player_sum, dealer_card, int(has_usable_ace))
                Q[state] = np.zeros(env.action_space.n)

# Set the number of episodes to run
num_episodes = 10000

# Set the maximum number of steps per episode
max_steps_per_episode = 50

# Set the exploration rate
exploration_rate = 1.0
max_exploration_rate = 1.0
min_exploration_rate = 0.01
exploration_decay_rate = 0.001

# Initialize win and loss counters
wins = 0
losses = 0

def state_to_key(state):
    if len(state) == 2:
        return str((state[0], state[1], False))  # Assuming no usable ace
    elif len(state) == 3:
        return str(state)
    else:
        print("Unexpected state format:", state)  # Add debug print
        return None  # Return None if state format is unexpected

# Train the agent
for episode in range(num_episodes):
    # Reset the environment and get the initial observation
    observation = env.reset()
    done = False
    rewards_current_episode = 0

    # Loop through the game until it's finished
    for step in range(max_steps_per_episode):
        # Convert observation to state for compatibility
        state = observation

        # Choose an action
        exploration_rate_threshold = np.random.uniform(0, 1)
        if exploration_rate_threshold > exploration_rate:
            key_state = state_to_key(state)
            action = np.argmax(Q.get(key_state, np.zeros(env.action_space.n)))
        else:
            action = env.action_space.sample()

        # Take the action and observe the result
        next_observation, reward, done, info, _ = env.step(action)

        # Update the Q-table
        key_state = state_to_key(state)
        key_next_state = state_to_key(next_observation)
        Q[key_state] = Q.get(key_state, np.zeros(env.action_space.n))
        Q[key_next_state] = Q.get(key_next_state, np.zeros(env.action_space.n))
        Q[key_state][action] = Q[key_state][action] + learning_rate * (reward + discount_factor * np.max(Q[key_next_state]) - Q[key_state][action])
        
        observation = next_observation
        rewards_current_episode += reward

        if done:
            break

    # Update the exploration rate
    exploration_rate = min_exploration_rate + (max_exploration_rate - min_exploration_rate) * np.exp(-exploration_decay_rate * episode)

# Evaluation loop
env = gym.make(game, render_mode="human")
env.reset()

# Initialize win and loss counters inside the loop
wins = 0
losses = 0

for i in range(num_episodes):
    observation = env.reset()
    done = False
    while not done:
        if len(observation) != 2 and len(observation) != 3:
            print("Unexpected observation format:", observation)  # Add debug print
            break

        # Convert observation to state for compatibility
        if len(observation) == 2:
            state = (observation[0], observation[1], False)  # Assuming no usable ace
        else:
            state = (observation[0], observation[1], int(observation[2]))

        # Convert state to a hashable type
        state_key = state_to_key(state)

        if state_key is None:
            break  # Exit the loop if state format is unexpected

        # Choose an action
        action = np.argmax(Q[state_key])

        # Take the action and observe the result
        observation, reward, done, info, _ = env.step(action)

        if done:
            if reward > 0:
                wins += 1
            else:
                losses += 1

    # Print wins and losses at each iteration
    print("Iteration:", i+1, "Wins:", wins, "Losses:", losses)

env.close()




