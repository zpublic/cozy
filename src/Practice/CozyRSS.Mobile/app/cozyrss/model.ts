export interface RSSContent {
  title?: string;
  time?: string;
  author?: string;
  content?: string;
}

export interface RSSSource {
  name?: string;
  url?: string;
  enable?: boolean;
  channel?: string;
  contents?: RSSContent[];
}
