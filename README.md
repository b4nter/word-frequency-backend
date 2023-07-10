### API to get how frequently words are being used in news outlet titles (word-frequency)

Upon request to get data from this api. It reads rss feed of various news outlets titles then counts how many times each word is being used.

Currently using this API to display most common words and do some basic analysis [here](https://github.com/b4nter/word-frequency-frontend)

Example of data returned:
```
[
  {
    "word": "something",
    "frequency": 3,
    "newsOutlet": "bbc"
  },
  {
    "word": "football",
    "frequency": 1,
    "newsOutlet": "guardian"
  },
  {
    "word": "game",
    "frequency": 7,
    "newsOutlet": "sky"
  }
]
```

