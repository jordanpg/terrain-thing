function getRotVal(%r)
{
	switch$(%r)
	{
		case "1 0 0 0": %r = 0;
		case "0 0 1 90": %r = 1;
		case "0 0 1 180": %r = 2;
		case "0 0 -1 90": %r = 3;
	}
	return %r;
}

function BuildingQueue::addBrick(%this, %pos, %uiName, %colorID, %rot, %tsConvert)
{
	if(%this.b $= "")
		%this.b = 0;
	if(%tsConvert !$= "")
	{
		%x = getWord(%pos, 1);
		%y = getWord(%pos, 0);
	}
	else
	{
		%x = getWord(%pos, 0);
		%y = getWord(%pos, 1);
	}
	%z = getWord(%pos, 2);
	if(%tsConvert !$= "")
	{
		%x /= getWord(%tsConvert, 0);
		%y /= getWord(%tsConvert, 1);
		%z /= getWord(%tsConvert, 2);
		if(getWordCount(%tsConvert) > 3 && getWord(%tsConvert, 3))
			%rot = getRotVal(%r);
	}
	%pos = %x SPC %y SPC %z;
	%this.bPos[%this.b] = %pos;
	%this.bBrick[%this.b] = %uiName;
	%this.bColor[%this.b] = %colorID;
	%this.bRot[%this.b] = %rot;
	%this.b++;
	return %this.b - 1;
}

function BuildingQueue::buildBrick(%this, %b)
{
	if(%this.b < %b)
		return;
	if(%this.curBrick !$= %brick = $uiNameTable[%this.bBrick[%b]])
		commandToServer('instantUseBrick', %this.curBrick = %brick);
	%pos = %this.bPos[%b];

	%relPos = vectorSub(%pos, %this.curPos);
	%this.curPos = %pos;
	%x = getWord(%relPos, 0);
	%y = getWord(%relPos, 1);
	%z = getWord(%relPos, 2);
	if(%this.lastColor !$= %color = %this.bColor[%b])
		commandToServer('useSprayCan', %this.lastColor = %color);
	commandToServer('shiftBrick', %x, %y, %z);
	//if(%this.bRot[%b] != 0 || striPos(%this.bBrick[%b], "Cube") != -1)
	//{
	for(%i = 0; %i < %this.bRot[%b]; %i++)
		commandToServer('rotateBrick', 1);
	//}
	commandToServer('plantBrick');
	for(%i = 0; %i < %this.bRot[%b]; %i++)
		commandToServer('rotateBrick', -1);

	if(%this.fromBottom)
		commandToServer('shiftBrick', 0, 0, mFloor($uiNameTable[%this.bBrick[%b]].brickSizeZ / -2));
}

function BuildingQueue::buildAllBricks(%this, %start, %time, %restart, %end, %home)
{
	if(!%this.ran || (%this.ran && %restart))
	{
		%this.ran = true;
		%this.curPos = "0 0 0";
		%this.curBrick = "";
		%this.lastColor = "";
		%this.curB = %start;
	}
	if(%end > %start)
		%this.endB = %start;
	else
		%this.endB = -1;
	%this.goToHome = %home;
	%this.time = %time;
	%this.buildTick();
}

function BuildingQueue::buildTick(%this)
{
	cancel(%this.bTick);
	if(%this.curB >= %this.b || (%this.curB >= %this.endB && %this.endB != -1))
	{
		if(%this.goToHome)
		{
			%pos = vectorSub("0 0 0", %this.curPos);
			commandToServer('shiftBrick', getWord(%pos, 0), getWord(%pos, 1), getWord(%pos, 2));
		}
		return;
	}
	%this.buildBrick(%this.curB);
	%this.curB++;
	if(%this.time > -1)
		%this.bTick = %this.schedule(%this.time, buildTick);
	else
		%this.buildTick();
}

function BuildingQueue::setTempBrick(%this, %i)
{
	%b = $uiNameTable[%this.bBrick[%b]];
	if(isObject(%b))
		commandToServer('instantUseBrick', %b);
}