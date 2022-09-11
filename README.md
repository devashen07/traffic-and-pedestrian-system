# traffic-and-pedestrian-system

This repo provides Unity source files for a traffic and pedestrian system. The system is a demonstration of four-way and three-way stop intersections and zebra crossing interactions. This project formed part of the technical interview for Fuzzy Logic Studio to demonstrate my coding abilities. I was given this scenario and completed it in an accumulative time of 10 hours.  

## How to use 

- Clone the repo using  ``` git clone https://github.com/devashen07/traffic-and-pedestrian-system.git ```
- Open Unity Hub and open the folder you just cloned. 
- Select **Unity Version 2020.3.28f1**, if you do not have this installed, please install it to not have any import and build problems. 
- Open the project, once in Unity you can just press play and see the system in action. 

## Demo Video

A demo video can be found following this link: 

https://drive.google.com/file/d/1p59I82z6fjla6QrHXjpmQIM5-m8YFkts/view?usp=sharing

To get the best viewing experience please DOWNLOAD the video :)

## 3D Model Assets 

For this project, open-source assets were downloaded and imported from the Unity Asset Store. The following packages were used: 

- Block People
- Simple Cars Pack 
- Low Poly Road Pack
- Building Apartment 
- Low Poly Buildings Lite 
- Pandazole Simple Game Low Poly Pack 
- Low Poly City Pack Collection


## System Description 

The system can be broken up into several different mechanisms as briefly described below: 

- ### Car and Pedestrian Path and Movement 

In the system, cars and pedestrians follow fixed routes according to a WayPoint system. Waypoints are a series of nodes that create a path that an object can be manipulated to follow. Creating a waypoint route manually can be a tedious task, therefore, through some online research I found a way to speed up the process by creating an editor tool within Unity. The associated scripts for the tool can be found in the _Editor Folder_. 

Cars and pedestrians were then integrated to follow their respective paths according to a navigator and movement was then added using vectors where speed was applied. The speed of each car or pedestrian is randomly selected from a range in order to make the system more realistic. 

- ### Car and Pedestrian Spawner

In order to have a realistic system many cars and pedestrians need to be added. Again, this task of manually adding these prefabs to the scene is time-consuming. As a result, I created a script to spawn cars and pedestrians at random waypoints along their respective routes. In addition, the number of cars or pedestrians to be spawned can be manipulated and certain waypoints (such as middle of intersections) can be deemed invalid for a car to spawn there. 

- ### Car Following Distance 

In order for cars not to collide with one another, a following distance had to be set. For this method, Raycasts were used. The raycasts are set to a specific length and placed at the front of a car. The raycasts are set to detect hits of other cars and when this event occurs the car that triggered the hit stops (as if slowing down) and continues when the hit is untriggered. 

- ### Four-Way and Three-Way Stop Interactions 

Four-way and three-way stop interactions work on a first come first serve basis, similar to First In First Out (FIFO). Hence the most appropiate data structure to use is a Queue. As cars enter an intersection, a box collider is present triggering ```OnTriggerEnter``` where a car is stopped and added to the queue, as updates roll out cars are dequeued creating an orderly movement of cars through an intersection. 

- ### Pedestrian Zebra Crossing 

A Zebra crossing works slightly different than a four-way stop, that is cars must yield to pedestrians if they are waiting to cross or are currently crossing the crosswalk. Cars do not yield for each other. As such, a box collider is used to detect whether a car or pedestrian is present and if a pedestrian is present cars are stopped. Using bool flags, cars will only pass through the crossing if the ```pedestrianWaiting``` and ```pedestrianWalking``` flags are set to false. 
