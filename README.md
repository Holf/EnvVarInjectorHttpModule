# EnvVarInjectorHttpModule
## An IIS HttpModule for injecting Server-side Environment Variables into JavaScript files

### Why do I need this?

Sometimes, all I need is my Azure WebApp to serve up static resources for my Single Page App, these being a single HTML file and a single JavaScript file. The JavaScript is a React or Angular App I've packaged using WebPack or Browserify. The HTML file is a simple shell which loads the JavaScript.

This is all well and good, except I want to be able to pass Environment Variables from the Server to browser clients. For example, in my Test Environment I might want my App to use a Test API. And in my Live Environment I want my App to use a Live API.

I have my 'API_URL' Environment Variable set appropriately in my different environments, but how do I pass this to the Javascript running in the browser?

### The traditional solution

Now, I could create an ASP.Net MVC App and use a Razor View to inject a JavaScript section into my HTML page which sets global variables for subsequently loaded scripts to use. But then again, I have such a simple setup here; all I need to do is to serve up a couple of static files. Do I really want to start bringing in MVC and Razor to my nice, clean React SPA just for some lowly Environment Variables?

### A simpler alternative

How about if I use an HTTP Module instead, that reads Environment Variables, and then injects code to set these into the start of my JavaScript file as it is being served up by IIS?

I don't want to inject all Environment Variables, so I'll need a way of identifying which ones are 'interesting'.

And I want to be able to choose a namespace for my global variables on the browser, so I can be sure they don't interfere with any other globals. (How about I use 'prcoess.env' as a default, which is what your typical React App would use?)

Oh, and I need to be able to target the correct JavaScript file(s).

### EnvVarInjectorHttpModule does exactly this

Clone this Repo and have a look at the Test Harness there within to see how it works. To do the equivalent, all you have to do is add the following to the WebPack distribution bundle that will be deployed to Azure:
* A Web.Config file which has the same elements that are present in the Test Harness example.
* A bin Folder which contains the EnvVarInjectorHttpModule.dll file that is output when you build the EnvVarInjectorHttpModule project.

(I could create a NuGet package that does this, I suppose, maybe I'll get round to that...)

And that's it! A genuinely two minute solution for all your Server-side to Browser Environment Variable needs.

