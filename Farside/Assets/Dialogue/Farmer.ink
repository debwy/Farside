INCLUDE globals.ink

{ cat:
- 0: -> main
- 1: -> accepted
- 2: -> final
}

=== main ===
~ cat = 1
EXTERNAL snapshotBatQuest()
Nyaaah? A new farmer joining the ranks? Finally! I've been waiting for this day for AGES.
Oh, you're not a village folk. Looks like I got my hopes up for nooothing....
But you look like such a stroong human. Why don't you come help out a little at the farm? I'm just sooo, so tired.
Just getting rid of <color=yellow>fifty of those pesky bats</color> would be purrrfect. I know you can do it.
* [...I'll do it]
<size=45>NYAAAH? You're mad! Fifty bats?!</size>
<size=35>Of all the luck, I just had to meet a crazy human today!</size>
<size=35>I'll just pretend the last ten seconds never happened...breathe in....breathe out...nyah.</size>

Ahem! Where was I? Ah, yes, I just hate those flying creatures. They've been stealing our crops recently. 
Why, I'm waiting right here until I see enough corpses of those insane creatures to sate my anger.

I think killing <color=yellow>ten of them</color> is fine. Yes, absolutely fine. 

-> END

=== accepted ===

Nyah, the mad human is back! Act natural, act natural...
//~ killed = batDeaths_global - snapshotBatDeaths_global
//~ killed = differenceInBatDeaths()

{ batDeaths_global:
- 0: You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 1: You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 2:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 3:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 4:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 5:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 6:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 7:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 8:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!
- 9:You haven't forgotten what I wanted, right? <color=yellow>Ten bats.</color> Destroyed.
Now off you go, human. Come back later - or never!

- else: Yes, you've killed enough bats. Loathe as I am to do it, I'll sing your praises to the town head. 
~ helped_global += 1
~ cat = 2 
}


-> END

=== final ===
Nyah, you're talking to me agaaain? 
I'll be sure to give the town head my approval. You did kill enough bats, after all.
Now shoo!

-> END





