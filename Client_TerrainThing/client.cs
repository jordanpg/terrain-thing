exec("./simplex.cs");
exec("./buildingqueue.cs");

$validHeight[0] = 15;
$validHeight[1] = 9;
$validHeight[2] = 3;
$validHeight[3] = 1;
$validHeights = 4;

$htuiNames = "1x1x5\t1x1x3\t1x1\t1x1F";

// $validHeight[0] = 15;
// $validHeight[1] = 3;
// $validHeight[2] = 1;
// $validHeights = 3;

// $htuiNames = "1x1x5\t1x1\t1x1F";

function searchWord(%list, %search)
{
	%ct = getWordCount(%list);
	for(%i = 0; %i < %ct; %i++)
		if(getWord(%list, %i) $= %search) return %i;

	return -1;
}

function initClientSimplex(%seed, %freq, %iter, %persist, %low, %high, %addheight)
{

	if(isObject($psvox))
	{
		$psvox.queue.delete();
		$psvox.simplex.delete();
		$psvox.terrain.delete();
		$psvox.delete();
	}

	$psVox = new ScriptObject(psVox);
	$psVox.queue = $queue = new ScriptObject(BuildingQueue);

	$psVox.initSimplex(%seed, %freq, %iter, %persist, %low, %high, %addheight);
}

function psVox::generateWorld(%this, %xMin, %yMin, %xMax, %yMax, %scale, %seed)
{
	%map = $psVox.terrain.newMap("height");
	%map.genHeightmap(%xMin, %yMin, %xMax, %yMax, %scale, %seed);

	return %map;
}

function psVox::queueMap(%this, %map, %xMin, %yMin, %xMax, %yMax)
{
	for(%y = %yMin; %y <= %yMax; %y++)
	{
		for(%x = %xMin; %x <= %xMax; %x++)
		{
			%h = mFloor(%map.height[%x, %y] + 0.5);
			%r = %h;
			%c = 0;

			echo("At (" @ %x @ ", " @ %y @ "):" SPC %h);
			for(%i = 0; %i < $validHeights; %i++)
			{
				if($validHeight[%i] <= %r)
				{
					echo($validheight[%i] SPC %r SPC %c);
					%ui = getField($htuiNames, %i);
					%this.queue.schedule(0, addBrick, %x SPC %y SPC (%c + mCeil($validHeight[%i] / 2)), %ui, -1, 0);
					echo("--> (" @ %x @ ", " @ %y @ ", " @ (%c + mCeil($validHeight[%i] / 2)) @ ")");	
					%r -= $validHeight[%i];
					%c += $validHeight[%i];
					%i--;

					echo("-->" SPC %r SPC %c);
					if(%r <= 0)
					{
						echo("Break out" SPC $validheight[%i+1] SPC %r SPC %c);
						break;
					}
				}
			}
		}
	}
}

function psVox::buildQueue(%this, %time)
{
	if(!%this.flag)
		$queue.setTempBrick(0);
	else
		$queue.buildAllBricks(0, %time, 1, -1, 1);

	%this.flag ^= 1;
}

function psVox::resetQueue(%this)
{
	%this.queue.delete();
	$psVox.queue = $queue = new ScriptObject(BuildingQueue);
}