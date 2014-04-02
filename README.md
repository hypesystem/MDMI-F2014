MDMI-F2014
==========

**Mining a Million Songs for Obvious Reasons and Profit.**

This is a project for the Data Mining (MDMI-F2014) course at the IT-University of Copenhagen.

The project group consists of:

- Sune Debel
- Magnus Stahl
- Niels Abildgaard

Questions
---------

Recommend songs based on the structure of the songs (unlike usual recommendation systems that are based on *what other users do*).

**Corollary:** Look at song lyrics, in addition to structure of songs.

The questions are in prioritized order. The idea is to start at the top and complete as many as possible.

1. Given a song, recommend songs that are *similar in structure*.
2. Given a user, recommend songs *similar in structure* those the user listens to.
3. Given a new song, estimate the *hotness* it will achieve once released.

### Recommend songs similar in structure

We want to somehow determine which songs are similar. We found two possible ways to do this:

- Use *k nearest neighbours* algorithm to find similar songs. This will be a slow approach, but may be optimized by using a kd-tree.
- Use clustering (of some sort) to classify the different songs. This will allow for better visualizations, and the classifications
  may be used later.

We choose to implement the second these options, as well as a U-Matrix for visualization.

### Recommend songs for user

Here, we will look at the different classes of songs. We will find classes of songs that are frequently listened to by the same people (frequent pattern mining).
Then, given a user, we find classes of the songs that user listens to. We can then recommend songs from the classes that other people, who listen to the same classes
of music as the given user, listen to.

The specific songs we recommend from new clusters can be decided by finding the ones closest to (k nearest neighbour) the songs the given has previously listened to
(in other clusters).

As such, we will get songs in new clusters (that other people with similar tastes like) that are similar in structure to songs the user already likes.

It's *math*gic!

### Estimate hotness a song will acheive

We discretize hotness into a couple of ranges. The goal is to predict which group a given song will fall in to once released.

This is done by using supervised learning (ID3?) to create a decision tree that can make the prediction.

We use, in addition to the normal attributes tied to the songs, the classes we found from clustering. Our theory is that there might be a correlation
between hotness and song class, and as such, using these classes might give us an early boost to information gain.