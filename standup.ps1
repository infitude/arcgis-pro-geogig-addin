# set locations
$repoPath = 'c:\temp\geogig\gold'
$dataPath = 'c:\temp\geogig\data'

# ensure an empty repository folder exists
if(Test-Path $repoPath) {
	Remove-Item $repoPath\* -recurse
}
else
{
	New-Item $repoPath -type Directory	
}

# ensure an empty local data folder exists
if(Test-Path $dataPath) {
	Remove-Item $dataPath\* -recurse
}
else
{
	New-Item $dataPath -type Directory	
}

# set current location to repo
Set-Location -Path $repoPath

# init the repo
&geogig init

# set globals
&geogig config --global user.name "Pete"
&geogig config --global user.email "pete@example.com"

# import map data, add and commit it
&geogig shp import 'C:\Users\pete\Documents\Visual Studio 2015\Projects\GeogigModule\UnitTestGeogigModule\MapData\citytown.shp'
&geogig add
&geogig commit -m "first version"

# start web api
&geogig serve













