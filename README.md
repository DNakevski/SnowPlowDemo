# Snowplow Demo App

_Simple Reactjs/.net Core shopping app that demonstrates usage of snowplow service to track events within the application_.

My initial idea was to make very simple Reactjs application that will use the snowplow javascript tracker in order to encapsulate the basic concepts and principles about event tracking using the [snowplow micro](https://github.com/snowplow-incubator/snowplow-micro/). However, while setting it up I encountered some issues and I was just not able to get it working with snowplow micro.  I spent some time checking [these examples](https://github.com/snowplow-incubator/snowplow-micro-examples) but no luck.  After some time I decided to just implement simple backend on the react app. I thought about going with Node.js, but because it's been a while since I did something in Node.js/Typescript and I am probably bit rusty on that terrain I decided to go with C# and use the [C# snowplow tracker](https://docs.snowplowanalytics.com/docs/collecting-data/collecting-from-own-applications/net-tracker/) for the sake of time and convenience :).  


## Snowplow micro setup

For setting up the snowplow micro I followed these [docs on github.](https://github.com/snowplow-incubator/snowplow-micro/). It was quite nice(by nice I mean simple :) ) and straightforward. Given that in this moment I am working on a windows machine I slightly modified the docker run command to match the windows directories syntax. I also modified the iglu.json file and added one additional schema repository for schemas that I "self-host" in the **iglu** folder on [this github repo](https://github.com/DNakevski/SnowPlowDemo/tree/master/iglu). But I will elaborate a bit more on that when I come to using the Unstructured events.

## Frontend shopping app

The frontend app is simple Reactjs application. It is located in the [**ClientApp** folder](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/ClientApp) in the repo and it consist of two main pages:
- **Products**, lists all the available product. Here we can chose some of the products and add them to cart.
- **Cart**, lists all the products that were added to the cart. We can place an order for the products in the cart and we can also remove products from the cart.

I also added very simple user authentication because I wanted to introduce some concept of users and I also believe tracking events like user login, logout and failed login attempts are very important. And also because it has been a while since I made some login forms..  login forms are always fun to make :).
The authentication is very simple, the logged  user is kept in **localStorage** and there are only 3 predefined users kept in-memory on the backend side that we can use for login:
- **Email:** scott@mail.com | **Pass:** Scott*123
-  **Email:** jack@mail.com | **Pass:** Jack*123
-  **Email:** alice@mail.com | **Pass:** Alice*123

For the sake of simplicity I didn't use any of the state management libraries like Redux or MobX. Just simple old-school local state in the components and passing state and events using props down the components hierarchy.
All the components are located in the [**src/components** folder](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/ClientApp/src/components) and there are also few simple [services](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/ClientApp/src/services) and [hoc](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/ClientApp/src/hoc) components to ease the things a bit.

### Backened (.net core API)
The backend API is written in .net Core (C#). It is simple Web API application with few very simple [controllers](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/Controllers), [services](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/Services) and in-memory [repositories](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/Repositories).
For simplicity, no database is used. To _simulate_ database usage, as mentioned previously, there are few in-memory [repositories](https://github.com/DNakevski/SnowPlowDemo/tree/master/SnowplowShoppingApp/Repositories) that keep the entities for the products, users and the orders and cart items. Each of the repositories has its own interface and implementation which are registered in the [Startup](https://github.com/DNakevski/SnowPlowDemo/blob/master/SnowplowShoppingApp/Startup.cs) class in order to be injected via Dependency Injection wherever they are needed...  And also for some decoupling of the code.

There is only one service, and that is the tracking service, that consists of [interface definition](https://github.com/DNakevski/SnowPlowDemo/blob/master/SnowplowShoppingApp/Services/ITrackingService.cs) and concrete [snowplow tracking implementation](https://github.com/DNakevski/SnowPlowDemo/blob/master/SnowplowShoppingApp/Services/SnowplowTrackingService.cs) of the service.
The configuration for the service is kept in the [appsettings.json](https://github.com/DNakevski/SnowPlowDemo/blob/master/SnowplowShoppingApp/appsettings.json) file (SnoplowConfig section) and it is registered in the [Startup](https://github.com/DNakevski/SnowPlowDemo/blob/master/SnowplowShoppingApp/Startup.cs) class so it can be injected through dependency injection in the service class where it is used for configuring and starting the tracker.

Because the .Net Snowplow Tracker is singleton instance and it needs to be started once, the service contains property wrapper called **TrackerInstance**. This property is responsible for configuring and starting the tracker instance if it is not started before returning it. In this way we can be ensured that the Tracker is always started before using it.

Rest of the tracking service is comprised of the tracking methods. There are six different tracking methods:
- **TrackUserLoginEvent** - Tracks the successful user login events. It uses the snowplow built-in **StructuredEvent** event with category "users" and action "user-login-success".
- **TrackUserLogoutEvent** - Tracks the user's logout event. It uses the snowplow built-in **StructuredEvent** event with category "users" and action "user-logout".
- **TrackUserUnsuccessfulLoginEvent** - tracks the user's unsuccessful login attempts. It uses the snowplow built-in **StructuredEvent** event with category "users" and action "user-login-fail".
- **TrackUserOrderEvent** - Tracks the event when user makes an order. It uses the snowplow built-in **EcommerceTransaction** event with all the products from the order stored as items of type **EcommerceTransactionItem**.
- **TrackPageViewEvent** - Tracks page view (API endpoints visits) using the built-in **PageView** event.
- **TrackCartActionEvent** - Tracks the **add to cart** and **remove from cart** events. It uses the **SelfDescribing  (Unstructured)** event type that require custom schema to be registered. For that matter, there is self-hosted repository with custom schema in this exact repo, located in the [iglu folder](https://github.com/DNakevski/SnowPlowDemo/tree/master/iglu). The repository is registered in the [**iglu.json** config file](https://github.com/DNakevski/SnowPlowDemo/blob/master/microconfig/iglu.json) which was used when snowplow micro is started as docker image. 

### Future steps
I really wanted to try out the javascript tracker and do some tracking on the frontend as well as combining tracking activities from frontend and backend. It is a bit shame that I didn't get it up and running on the first run. I saw that it has some really nice and interesting features. I will be continuing my efforts on that part and all the changes will be added to this repo.
Another thing that I want to try out whenever I get bit more time is implementing custom context in the events as well as custom events.  It really broadened my perspective on what all can be traced as event from reading some of the docs. Those experiments will also be added to this repo.

### Conclusion 
It is a bit late and I might be missing something :). That being said, I am really looking forward talking to you guys so I can elaborate a bit more on the solution and get some feedback from you as well. In the meantime I will update the repo if there are some pending changes.
I really like the snowplow analytics tool. It was a positive surprise to see the broad range of platforms Snowplow supports. Especially given the fact that there are twenty(and something) engineers working in Snowplow. Also, the efforts that the guys are making on the open source project and keeping all those repos nice and clean is for admiration(kudos). It is privilege to work and grow in environment like that.

Regards and looking forward to your feedback.