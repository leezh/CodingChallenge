# Answers

## 1. How did you make the code more modular and testable?

> did you use any [SOLID](https://en.wikipedia.org/wiki/SOLID) design principles? if so, which ones?

By standardising the query code into a `IHotelSource` interface and inheriting
from there we can easily combine their outputs while still maintaining
source-specific implementations. In doing so the specfic implementation for
each source is no longer `HotelsController.Get()`'s concern.

## 2. The downstream endpoints can be quite slow. How can we speed up our API?

> Feel free to demonstrate an example in the code.

I utilised the async nature of the query to call the two endpoints in parallel
and collect them later. This is normally fine for a few endpoints but
eventually - due to resource strain on maintaining simultaneous connections -
it would be worth running these queries in batches.

Depending on how up-to-date the data needs to be, we can also look into adding
caching for the sources.

## 3. If you added any tests to the code, which approach did you use and why?

Considering there was already an xunit test framework project already I
decided to extend it and actually test that `HotelsController.Get()` returns
the correct result.

Further tests could be added mocking upstream sources being down, and a cursory
look at mocking libraries such as https://github.com/richardszalay/mockhttp
could be used but would only be necessary once graceful error handling while
fetching data is implemented.

## 4. How would you add any new data sources to the `/api/hotels/` endpoint in the future?

New sources could be added by deriving from `IHotelSource` and implementing the
`IHotelSource.GetHotels()` function. It would then need to be added to the list
of sources in `HotelsController.Get()`.

## 5. Can your solution easily scale with new providers of hotel data being added?

When scaling up, adding more classes for each and every hotel may get out of
hand quickly. It would be worth grouping similar sources such as hotel
groups that use a common Building Management Software, like
`HoneywellHotelSource` and having it read from a configuration to retrieve
all of its sources.

On the other hand more sources may result in too many simultaneous connections
so eventually the use of `Enumerable.Chunk()` to split the source into batches
may be required.

## 6. If you have clients using the current version of the API, how would you approach this refactoring task?

I approached this by trying to ensure `/api/Hotels/` maintains the same output
schema. However, not all data is present in the `Residenza` source so some
fields were made `null` and may result in unexpected behaviour with clients. In
such case it would be worth placing the refactored code in a versioned path like
`/api/v2/Hotels/` instead.

## 7. If you had to host this API in a production envirnment, describe the steps you would take to do this

Microsoft provides Docker files for running ASP programs, and example of which
is here:

https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile

I would take that docker file and adapt it for the `API/API.csproj` project.

I would then go to the company's Dockerhub account where I would create a new
project. There I would build and push the docker image using said project's tag.

Finally, for a simple single instance, I would go on the the company's cloud
provider and create a container using the project's tag (assuming Dockerhub
integration has been set up already).

For multiple instances I would look into the use of kubernetes, docker swarm or
said cloud provider's scalable containers solutions.
