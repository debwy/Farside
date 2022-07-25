INCLUDE globals.ink

{ quest_global == 0: -> main | -> quest }

=== main ===
~ quest_global = 1
Welcome, stranger, to our peaceful village.
I am the town head - that is to say, everything that goes on here is known to me.
Of course, the moment you stepped into our domain, I have known your true purpose.
If you would like to enter our village and learn its secrets, you must first complete a rite of passage.
Every traveler must prove their worth by aiding our folk in their troubles.
After all, we are but a small community, and we need each other's help to survive.
Come speak to me again after you have gained <color=yellow>five of our people's vote of confidence</color>.
Only then will you be allowed to set foot in our beloved village.

-> END

=== quest ===
Come speak to me when you have our people's vote of confidence. 

* [What's my progress?]

{ helped_global:
- 0: None of the village folk has spoken about you.
Are you <i>truly</i> determined to enter the village? If so, you have a long way to go.
Remember, help <color=yellow>five of our townsfolk out and receive their blessings</color>. 
Then, come back and speak to me.
- 1: One of the townsfolk regards you highly! Quite an improvement. 
But of course, you have a long way to go. 
I will only consider you fit to enter our village with <color=yellow>four more recommendations</color>.
Come back again once you have done that.
- 2: Two villagers have come to me with their recommendations. 
But alas, it takes more than two to tango. 
If I say so, then it is so. Get moving, human. <color=yellow>Three more recommendations to go</color>!
- 3: So you've gained the approval of three of our townsfolk. You do have promise in you, outsider.
However, you still have <color=yellow>two more recommendations to go</color>!
- 4: Your rite of passage is almost done. As the town head, I couldn't be happier to welcome a new village folk.
But don't count your crows before they've hatched! 
<color=yellow>One more recommendation,</color> and you shall be treated as one of us.
- else: Hm, you've made it this far! Congratulations.
This is certainly a testament to your abilities and perseverance. 
I hope you enjoyed helping out our townsfolk as much as I enjoyed seeing you complete their quests!
I'll see to it that you get a village permit right away. 
<i>Once again, a warm welcome to our village of Farside...</i> #end:yes
}

-> END

* [Alright]
Get moving, human. Time's ticking!

-> END













