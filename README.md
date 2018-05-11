# Movie_recommendation
Collaborative filtering (CF) is a technique used by recommender systems. Here a c# vs project to suggest Movie to user based on Collaborative filtering (CF) 

Project Description
 visual studio c# to make this project, UI was made using windows form. I made a another sorted by userid txt file from ratings.txt before running the operation.

I create few classes to take movieid, userids and their watched movie, and movieid and their rating from users. 
I made customized list with these classes.
I run some query using where clauses to find out same movie by other users. And calculate the average rating for that movie with that users

If I skip the query complexity on the lists, it took n3 complexity to get the predicted score for a movie.
Screenshot:
 

 
Readme:

1. Import in visual studio and run
or 
go to bin folder ->debug and click on exe file. 
remember 3 txt file need to be present in bin folder
there are 3txt file movie_titles.txt , ratings.txt, ratingusersorted.txt

2.Ui will comeout after 20-45s (for reading and loading the data) depend on processor,

3. First listbox will contain userlist, secondlist contain movielist

4. Select a userid from firstlistbox

5. After a few seconds 3rd listbox with that userids watched movie and 4th listbox with that user unwatchedmovie

6. Select a movie from unwatchedmovielist and after few second below listbox a red text will comeup with the predtiction score

7. There is a dropdown box to select year (suggest to select year after 1990) after selecting year after max 3-4 minuted all movie from that year with the prediction score will come in last listbox.

8. Can do these operations again and again, sometime it took much time to calculate. plz wait that time


