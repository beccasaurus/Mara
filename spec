#! /bin/bash

if [[ -z "$1" ]]; then
	echo "Running all specs"
	./packages/NUnit.2.5.7.10213/Tools/nunit-console.exe -labels Mara.Specs/bin/Debug/Mara.Specs.dll
	exit $?
fi

if [[ -n "$1" ]]; then
	echo "Running $1"
	./packages/NUnit.2.5.7.10213/Tools/nunit-console.exe -labels -run=$1 Mara.Specs/bin/Debug/Mara.Specs.dll
	exit $?
fi
