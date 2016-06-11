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
  news?: number;
  channel?: string;
  contents?: RSSContent[];
}
