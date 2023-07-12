using System.Collections.Generic;
using Numpy;
using Python.Runtime;
using NP = Numpy.np;

namespace Sharpcubes;

internal class Polycube
{
	public Guid Id { get; set; }

	public int Size { get; set; }
	
	public List<int[]> Cubes = new List<int[]>();

	public Polycube(int size)
	{
		if (size < 1)
			throw new ArgumentException("Size must be greater than 0.", nameof(size));

		Size = size;
		Id = Guid.NewGuid();

		PythonEngine.BeginAllowThreads();

		Task.Run(() =>
		{
			// When running on different threads you must lock!
			using (Py.GIL())
			{
				var arr = (byte)NP.array(new int[] { 1, 1, 1 });
			}
		}).Wait();
	}
}

/*
 * # Empty list of new n-polycubes
    polycubes = []
    polycubes_rle = set()

    base_cubes = generate_polycubes(n-1, use_cache)

    for idx, base_cube in enumerate(base_cubes):
        # Iterate over possible expansion positions
        for new_cube in expand_cube(base_cube):
            if not cube_exists_rle(new_cube, polycubes_rle):
                polycubes.append(new_cube)
                polycubes_rle.add(rle(new_cube))

        if (idx % 100 == 0):               
            perc = round((idx / len(base_cubes)) * 100,2)
            print(f"\rGenerating polycubes n={n}: {perc}%", end="")

    print(f"\rGenerating polycubes n={n}: 100%   ")
*/