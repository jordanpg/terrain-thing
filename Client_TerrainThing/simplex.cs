function SimplexNoise(%seed)
{
	%g3 = "1 1 0\t-1 1 0\t1 -1 0\t-1 -1 0\t1 0 1\t-1 0 1\t1 0 -1\t-1 0 -1\t0 1 1\t0 -1 1\t0 1 -1\t0 -1 -1";
	// %p = "151\t160\t137\t91\t90\t15" TAB
	// "131\t13\t201\t95\t96\t53\t194\t233\t7\t225\t140\t36\t103\t30\t69\t142\t8\t99\t37\t240\t21\t10\t23" TAB
	// "190\t6\t148\t247\t120\t234\t75\t0\t26\t197\t62\t94\t252\t219\t203\t117\t35\t11\t32\t57\t177\t33" TAB
	// "88\t237\t149\t56\t87\t174\t20\t125\t136\t171\t168\t68\t175\t74\t165\t71\t134\t139\t48\t27\t166" TAB
	// "77\t146\t158\t231\t83\t111\t229\t122\t60\t211\t133\t230\t220\t105\t92\t41\t55\t46\t245\t40\t244" TAB
	// "102\t143\t54\t65\t25\t63\t161\t1\t216\t80\t73\t209\t76\t132\t187\t208\t89\t18\t169\t200\t196" TAB
	// "135\t130\t116\t188\t159\t86\t164\t100\t109\t198\t173\t186\t3\t64\t52\t217\t226\t250\t124\t123" TAB
	// "5\t202\t38\t147\t118\t126\t255\t82\t85\t212\t207\t206\t59\t227\t47\t16\t58\t17\t182\t189\t28\t42" TAB
	// "223\t183\t170\t213\t119\t248\t152\t2\t44\t154\t163\t70\t221\t153\t101\t155\t167\t43\t172\t9" TAB
	// "129\t22\t39\t253\t19\t98\t108\t110\t79\t113\t224\t232\t178\t185\t112\t104\t218\t246\t97\t228" TAB
	// "251\t34\t242\t193\t238\t210\t144\t12\t191\t179\t162\t241\t81\t51\t145\t235\t249\t14\t239\t107" TAB
	// "49\t192\t214\t31\t181\t199\t106\t157\t184\t84\t204\t176\t115\t121\t50\t45\t127\t4\t150\t254" TAB
	// "138\t236\t205\t93\t222\t114\t67\t29\t24\t72\t243\t141\t128\t195\t78\t66\t215\t61\t156\t180";
 
	%obj = new ScriptObject()
			{
				class = "SimplexNoise";
				p = %p;
			};
	// for(%i = 0; %i < 512; %i++)
	// 	%obj.perm[%i] = getField(%p, %i & 255);
	%ct = getFieldCount(%g3);
	for(%i = 0; %i < %ct; %i++)
		%obj.grad3[%i] = getField(%g3, %i);
	%obj.setSeed(%seed);
	return %obj;
}

function SimplexNoise::setSeed(%this, %seed)
{
	if(!%seed)
		%seed = 0;

	if(%seed $= %this.seed)
		return;

	%p = "151\t160\t137\t91\t90\t15" TAB
	"131\t13\t201\t95\t96\t53\t194\t233\t7\t225\t140\t36\t103\t30\t69\t142\t8\t99\t37\t240\t21\t10\t23" TAB
	"190\t6\t148\t247\t120\t234\t75\t0\t26\t197\t62\t94\t252\t219\t203\t117\t35\t11\t32\t57\t177\t33" TAB
	"88\t237\t149\t56\t87\t174\t20\t125\t136\t171\t168\t68\t175\t74\t165\t71\t134\t139\t48\t27\t166" TAB
	"77\t146\t158\t231\t83\t111\t229\t122\t60\t211\t133\t230\t220\t105\t92\t41\t55\t46\t245\t40\t244" TAB
	"102\t143\t54\t65\t25\t63\t161\t1\t216\t80\t73\t209\t76\t132\t187\t208\t89\t18\t169\t200\t196" TAB
	"135\t130\t116\t188\t159\t86\t164\t100\t109\t198\t173\t186\t3\t64\t52\t217\t226\t250\t124\t123" TAB
	"5\t202\t38\t147\t118\t126\t255\t82\t85\t212\t207\t206\t59\t227\t47\t16\t58\t17\t182\t189\t28\t42" TAB
	"223\t183\t170\t213\t119\t248\t152\t2\t44\t154\t163\t70\t221\t153\t101\t155\t167\t43\t172\t9" TAB
	"129\t22\t39\t253\t19\t98\t108\t110\t79\t113\t224\t232\t178\t185\t112\t104\t218\t246\t97\t228" TAB
	"251\t34\t242\t193\t238\t210\t144\t12\t191\t179\t162\t241\t81\t51\t145\t235\t249\t14\t239\t107" TAB
	"49\t192\t214\t31\t181\t199\t106\t157\t184\t84\t204\t176\t115\t121\t50\t45\t127\t4\t150\t254" TAB
	"138\t236\t205\t93\t222\t114\t67\t29\t24\t72\t243\t141\t128\t195\t78\t66\t215\t61\t156\t180";

	for(%i = 0; %i < 512; %i++)
	{
		%shift = (%i + %seed) % 512;
		%this.perm[%i] = getField(%p, %shift & 255);
	}

	%this.seed = %seed;
}

function SimplexNoise::noise2d(%this, %x, %y, %seed)
{
	if(%seed !$= "")
		%this.setSeed(%seed);
	%sq3 = mPow(3, 0.5);
	%f2 = 0.5 * (%sq3 - 1);
	%s = (%x + %y) * %f2;
	%i = mFloor(%x + %s);
	%j = mFloor(%y + %s);

	%g2 = (3 - %sq3) / 6;
	%t = (%i + %j) * %g2;
	%x0 = %x - (%i - %t);
	%y0 = %y - (%j - %t);

	if(%x0 > %y0)
	{
		%i1 = 1;
		%j1 = 0;
	}
	else
	{
		%i1 = 0;
		%j1 = 1;
	}

	%x1 = %x0 - %i1 + %g2;
	%y1 = %y0 - %j1 + %g2;
	%x2 = %x0 - 1 + 2 * %g2;
	%y2 = %y0 - 1 + 2 * %g2;

	%ii = %i & 255;
	%jj = %j & 255;
	%gi0 = %this.perm[%ii + %this.perm[%jj]] % 12;
	// echo(1 SPC %gi0);
	%gi1 = %this.perm[%ii + %i1 + %this.perm[%jj + %j1]] % 12;
	// echo(2 SPC %gi1);
	%gi2 = %this.perm[%ii + 1 + %this.perm[%jj + 1]] % 12;
	// echo(3 SPC %gi2);

	%t0 = 0.5 - %x0 * %x0 - %y0 * %y0;
	if(%t0 < 0)
		%n0 = 0;
	else
	{
		%t0 = %t0 * %t0;
		%n0 = %t0 * %t0 * simp_dot2d(%this.grad3[%gi0], %x0, %y0);
	}

	%t1 = 0.5 - %x1 * %x1 - %y1 * %y1;
	if(%t1 < 0)
		%n1 = 0;
	else
	{
		%t1  = %t1 * %t1;
		%n1 = %t1 * %t1 * simp_dot2d(%this.grad3[%gi1], %x1, %y1);
	}

	%t2 = 0.5 - %x2 * %x2 - %y2 * %y2;
	if(%t2 < 0)
		%n2 = 0;
	else
	{
		%t2 = %t2 * %t2;
		%n2 = %t2 * %t2 * simp_dot2d(%this.grad3[%gi2], %x2, %y2);
	}

	return 70 * (%n0 + %n1 + %n2);
}

function SimplexNoise::noise3d(%this, %x, %y, %z, %seed)
{
	if(%seed !$= "")
		%this.setSeed(%seed);
	%f3 = (1 / 3);
	%s = (%x + %y + %z) * %f3;
	%i = mFloor(%x + %s);
	%j = mFloor(%y + %s);
	%k = mFloor(%z + %s);
 
	%g3 = (1 / 6);
	%t = (%i + %j + %k) * %g3;
	%x0 = %x - (%i - %t);
	%y0 = %y - (%j - %t);
	%z0 = %z - (%k - %t);
 
	if(%x0 >= %y0)
	{
		if(%y0 >= %z0)
		{
			%i1 = 1;
			%j1 = 0;
			%k1 = 0;
			%i2 = 1;
			%j2 = 1;
			%k2 = 0;
		}
		else if(%x0 >= %z0)
		{
			%i1 = 1;
			%k1 = 0;
			%j1 = 1;
			%i2 = 0;
			%k2 = 1;
			%j2 = 1;
		}
		else
		{
			%i1 = 0;
			%k1 = 0;
			%j1 = 1;
			%i2 = 1;
			%k2 = 0;
			%j2 = 1;
		}
	}
	else
	{
		if(%y0 < %z0)
		{
			%i1 = 1;
			%j1 = 0;
			%k1 = 0;
			%i2 = 1;
			%j2 = 1;
			%k2 = 0;
		}
		else if(%x0 < %z0)
		{
			%i1 = 0;
			%k1 = 1;
			%j1 = 0;
			%i2 = 0;
			%k2 = 1;
			%j2 = 1;
		}
		else
		{
			%i1 = 0;
			%k1 = 1;
			%j1 = 0;
			%i2 = 1;
			%k2 = 1;
			%j2 = 0;
		}
	}
 
	%x1 = %x0 - %i1 + %g3;
	%y1 = %y0 - %j1 + %g3;
	%z1 = %z0 - %k1 + %g3;
	%x2 = %x0 - %i2 + 2*%g3;
	%y2 = %y0 - %j2 + 2*%g3;
	%z2 = %z0 - %k2 + 2*%g3;
	%x3 = %x0 - 1 + 3*%g3;
	%y3 = %y0 - 1 + 3*%g3;
	%z3 = %z0 - 1 + 3*%g3;
 
	%ii = %i & 255;
	%jj = %j & 255;
	%kk = %k & 255;
	%gi0 = %this.perm[%ii + %this.perm[%jj + %this.perm[%kk]]] % 12;
	%gi1 = %this.perm[%ii + %i1 + %this.perm[%jj + %j1 + %this.perm[%kk + %k1]]] % 12;
	%gi3 = %this.perm[%ii + 1 + %this.perm[%jj + 1 + %this.perm[%kk + 1]]] % 12;
 
	%t0 = 0.5 - %x0*x0 - %y0*%y0 - %z0*%z0;
	if(%t0 < 0)
		%n0 = 0;
	else
	{
		%t0 = %t0 * %t0;
		%n0 = %t0 * %t0 * simp_dot(%this.grad3[%gi0], %x0, %y0, %z0);
	}
 
	%t1 = 0.5 - %x1 * %x1 - %y1 * %y1 - %z1 * %z1;
	if(%t1 < 0)
		%n1 = 0;
	else
	{
		%t1 = %t1 * %t1;
		%n1 = %t1 * %t1 * simp_dot(%this.grad3[%gi3], %x1, %y1, %z1);
	}
 
	%t2 = 0.5 - %x2 * %x2 - %y2 * %y2 - %z2 * %z2;
	if(%t2 < 0)
		%n2 = 0;
	else
	{
		%t2 = %t2 * %t2;
		%n2 = %t2 * %t2 * simp_dot(%this.grad3[%gi3], %x2, %y2, %z2);
	}
 
	%t3 = 0.5 - %x3 * %x3 - %y3 * %y3 - %z3 * %z3;
	if(%t3 < 0)
		%n3 = 0;
	else
	{
		%t3 = %t3 * %t3;
		%n3 = %t3 * %t3 * simp_dot(%this.grad3[%gi3], %x3, %y3, %z3);
	}
 
	return 32 * (%n0 + %n1 + %n2 + %n3);
}

function simp_dot2d(%g, %x, %y)
{
	%g0 = getWord(%g, 0) * %x;
	%g1 = getWord(%g, 1) * %y;
	return %g0 + %g1;
}

function simp_dot(%g, %x, %y, %z)
{
	%g0 = getWord(%g, 0) * %x;
	%g1 = getWord(%g, 1) * %y;
	%g2 = getWord(%g, 2) * %z;
	return %g0 + %g1 + %g2;
}

function psVox::initSimplex(%this, %seed, %freq, %iter, %persist, %low, %high, %addheight)
{

	if(isObject(%this.terrain))
		%this.terrain.delete();
	if(isObject(%this.simplex))
		%this.simplex.setSeed(%seed);
	else
		%this.simplex = SimplexNoise(%seed);

	%low = (%low >= 0 ? %low : 0);
	if(%high <= %low)
	{
		if(255 < %low)
			%high = %low + 64;
		else
			%high = 255;
	}
	%this.terrain = new ScriptObject()
					{
						class = "psVoxTerrain";
						noise = %this.simplex;
						parent = %this;

						seed = %this.simplex.seed;
						freq = (%freq > 0 ? %freq : (1 / 16));
						iter = (%iter > 0 ? %iter : 8);
						persist = (%persist > 0 ? %persist : 0.5);
						low = %low;
						high = %high;
						// scale = (%scale > 0 ? %scale : 0.02);
						add = %addheight;
					};
	%this.terrain.maps = new SimGroup();
	return %this.terrain;
}

function psVoxTerrain::getHeight(%this, %x, %y, %seed)
{
	%maxAmp = 0;
	%amp = 1;
	%f = %this.freq;
	%noise = 0;

	for(%i = 0; %i < %this.iter; %i++)
	{
		%noise += %this.noise.noise2d(%x * %f, %y * %f, %seed) * %amp;
		%maxAmp += %amp;
		%amp *= %this.persist;
		%f *= 2;
	}

	%noise /= %maxAmp;
	%noise = %noise * (%this.high - %this.low) / 2 + (%this.high + %this.low) / 2;
	return %noise;
}

function psVoxTerrain::getDensity(%this, %x, %y, %z, %seed, %norm, %nof)
{
	%f = (!%nof ? %this.freq : 1);
	%noise = %this.noise.noise3d(%x * %f, %y * %f, %z * %f, %seed);
	if(%norm)
		%noise = (%noise + 1) / 2;
	return %noise;
}

function psVoxTerrain::onRemove(%this)
{
	if(isObject(%this.maps))
		%this.maps.delete();
}

function psVoxTerrain::newMap(%this, %name, %keep)
{
	if(isObject(%this.map[%name]))
	{
		if(%keep)
			return %this.map[%name];
		%this.map[%name].delete();
	}

	%map = new ScriptObject("terrainMap_" @ %name)
			{
				class = "psVoxTerrainMap";
				name = %name;
				parent = %this;
			};
	%this.map[%name] = %map;
	%this.maps.add(%map);
	return %map;
}

function psVoxTerrainMap::addHeight(%this, %x, %y, %scale, %seed)
{
	%this.height[%x, %y] = (%this.parent.getHeight(%x, %y, %seed) * %scale) + %this.parent.add;
}

function psVoxTerrainMap::genHeightmap(%this, %xMin, %yMin, %xMax, %yMax, %scale, %seed)
{
	// $bloog = true;
	for(%y = %yMin; %y <= %yMax; %y++)
	{
		for(%x = %xMin; %x <= %xMax; %x++)
		{
			%this.addHeight(%x, %y, %scale, %seed);
		}
	}
}