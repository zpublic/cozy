export interface RSSContent {
  title?: string;
  url?: string;
  time?: string;
  author?: string;
  content?: string;
}

export interface RSSSource {
  name?: string;
  url?: string;
  enable?: boolean;
  news?: number;
  channel?: string;
  contents?: RSSContent[];
}
