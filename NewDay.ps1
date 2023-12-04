param (
    [int]$Day = (Get-Date).Day
)

if (Test-Path -Path "./Solutions/Day$($Day).cs") {
    throw "Day already exist"
}

if (-not ($env:AOC_Token)) {
    throw "Please store Access Token as Env Variable AOC_Token"
}

Write-Output "Copying Day.example to ./Solutions/Day$($Day).cs"
$null = Copy-Item -Path "./Solutions/Day.example" -Destination "./Solutions/Day$($Day).cs"

Write-Output "Replacing {{CLASSNAME}} with Day$($Day) in ./Solutions/Day$($Day).cs"
$null = (Get-Content -Path "./Solutions/Day$($Day).cs") -replace "{{CLASSNAME}}", "Day$($Day)" | Out-File -FilePath "./Solutions/Day$($Day).cs" -Encoding utf8

Write-Output "Downloading input file"
$null = Invoke-WebRequest -Uri "https://adventofcode.com/2023/day/$($Day)/input" -OutFile "./Solutions/Day$($Day)Input.txt" -Headers @{ "Cookie" = "session=$($env:AOC_Token)" }