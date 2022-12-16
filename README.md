# The RewardMatic 4000
Welcome to the RewardMatic 4000 code test!

This contains two implementations of `IUser`, an interface for a fictitious user of our system. One, `User`, we believe operates correctly. The other, `Misuser`, not so much. Both allow users to score points and receive rewards for scoring a given number of points. You can see the operation of some of this in the test that checks that the user's score increments correctly.

In this test, you will write a suite of tests for the interface described in `IUser` and run them on both `User` and `Misuser` objects. We would expect `User` to pass all your tests, but nobody's perfect!

You will then describe the problems with `Misuser` and, if applicable, `User`.

The system will use data from **rewards.json**. You can use the data in `RewardGroup.Available` in your tests.

We recommend you read the entire task description before starting. You are free to structure the tests however you wish.

## How to submit

Please create a branch of `main` and work there. When you're ready, please create a pull request from this branch back to `main`. In this task, you will need to describe the results of some tests; you can do this by creating a markdown in this repository (or editing this one) or you can send us a message containing your findings directly.

## Step 1: write tests for `IUser`

An `IUser` object is a toy version of a user. Each user has a score; their score can increase, but not decrease, and this is done through the `Update(int)` function. You can see the behaviour in `TestScoreIncrementsCorrectly()` in the `UserRewardUnitTest` class.

A `Reward` object is a treat the user gets when their score passes a threshold: it contains the score differential they need to achieve get the reward, and the message the user sees when they are rewarded.

A quick note on how to understand the score differentials: `RewardGroup.Available[0].Rewards[0]` has a `ScoreDifferential` of 200, so the user needs to get over 200 points to win this reward. `RewardGroup.Available[0].Rewards[1]` has a `ScoreDifferential` of 300, so the user needs to get an *additional* 300 points to win this reward: that's a *total* of 500. You can think of the `ScoreDifferential` like the "price" of the reward, and as the user accumulates a greater score, they can "spend" their score to get new rewards. You can't spend the same points on more than one reward!

`IUser.GetRewardInProgress()` should always return the reward the user is working towards, unless they have achieved them all; then it should return `null`. If not `null`, `IUser.GetRewardInProgress().Group` should in turn give the `RewardGroup` for the `Reward` in progress.

`IUser.GetLatestRewardReceived()` should always return the last reward the user received, unless they haven't received any; then it should return `null`. If not `null`, `IUser.GetLatestRewardReceived().Group` should in turn give the `RewardGroup` for the latest `Reward` received.

Finally, `IUser.GetLatestRewardGroupCompleted()` should give the latest `RewardGroup` the user has *entirely completed*. For example: if a user has completed three of the first group of rewards, then that user hasn't completed *any* `RewardGroup`s. If a user has completed all six rewards in the first group, or if a user has completed three of the second group of rewards, then that user has completed the *first* `RewardGroup`. When the user has completed all the `Reward`s, then they have completed the *last* `RewardGroup`.

## Step 2: run the tests and describe what you find

We expect that `User` will behave correctly, and `Misuser` won't. Please test both classes using your tests and write up what you find. Feel free to iterate through these steps multiple times if you like!

## Good luck!
