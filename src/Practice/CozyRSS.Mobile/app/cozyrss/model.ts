export interface RSSContent {
  title?: string;
  url?: string;
  time?: string;
  author?: string;
  content?: string;
  read?: boolean;
}

export interface RSSSource {
  name?: string;
  url?: string;
  enable?: boolean;
  news?: number;
  channel?: string;
  contents?: RSSContent[];
}

export interface FeedItem {
  author?: string;
  categories?: any[];
  content?: string;
  description?: string;
  guid?: string;
  link?: string;
  pubDate?: string;
  thumbnail?: string;
  title?: string;
}

export interface FeedObject {
  author?: string;
  description?: string;
  image?: string;
  link?: string;
  title?: string;
}
